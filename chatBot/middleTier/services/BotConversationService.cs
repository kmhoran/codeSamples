using Newtonsoft.Json;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sabio.Web.Services
{
    public class BotConversationService : BaseService
    {

        //- Built-in bot identity -- Not stored in DB//+++++++++++++++++++++++++++++++++++++++++++++
        protected static string _botId = "SYSTEMBOT";
        protected static string _botFullName = "QuoteMule Helper";
        protected static string _botUrl = "http://www.freevectors.net/files/large/FlatBlueRobotVectorIllustration.jpg";
        protected static int _conversationId = 0;
        protected static string _botCompanyName = "QuoteMule";

        //- Authentication key for messaging the QM chatbot.
        private static string _botAuthKey = "AuthKey";

        private static string _apiConversationId = "";

        //- Get methods//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected static string BotId
        {
            get { return _botId; }
        }

        protected static string BotFullName
        {
            get { return _botFullName; }
        }

        protected static string BotUrl
        {
            get { return _botUrl; }
        }

        protected static int BotConversationId
        {
            get { return _conversationId; }
        }

        protected static string BotCompanyName
        {
            get { return _botCompanyName; }
        }

        private static string BotAuthKey
        {
            get { return _botAuthKey; }
        }

        private static string ApiConversationId
        {
            get { return _apiConversationId; }
            set { _apiConversationId = value; }

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static BotConversationDomain BuildBotProfile()
        {
            var botProfile = new BotConversationDomain
            {
                ConversationId = BotConversationId,
                BotId = BotId,
                BotUrl = BotUrl,
                BotFullName = BotFullName,
                BotCompanyName = BotCompanyName
            };

            return botProfile;
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static int InsertMessage(MessageInsertRequest model)
        {
            int messageId = 0;

            //- This design expects the non-human id to be left blank.
            bool isSender = (model.SenderId != null && model.SenderId != "");

            string humanId = isSender ? model.SenderId : model.ReceiverId;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.SystemMessage_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@humanId", humanId);
                    paramCollection.AddWithValue("@content", model.Content);
                    paramCollection.AddWithValue("@isSender", isSender);

                    SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@id"].Value.ToString(), out messageId);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return messageId;
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public static List<MessageDomain> GetMessagesByUserId(string userId)
        {
            //- Retrieve List<MessageDomain>
            var messageList = new List<MessageDomain>();

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.SystemMessage_GetByUserId"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@userId", userId);
               },
                 map: delegate (IDataReader reader, short set)
                 {
                     MessageDomain singleMessage = new MessageDomain();
                     int startingIndex = 0; //startingOrdinal

                     //- Default human credentials as Sender info. This will be reviewed soon.
                     singleMessage.MessageId = reader.GetSafeInt32(startingIndex++);
                     singleMessage.SenderId = reader.GetSafeString(startingIndex++);
                     singleMessage.Content = reader.GetSafeString(startingIndex++);
                     singleMessage.CreateDate = reader.GetSafeDateTime(startingIndex++);
                     singleMessage.SenderFullName = reader.GetSafeString(startingIndex++);
                     singleMessage.SenderUrl = reader.GetSafeString(startingIndex++);

                     bool isSender = reader.GetSafeBool(startingIndex++);

                     //- For each message supply appropriate "autor" identity values. Either Human or bot.
                     MessageDomain messageToAdd = BuildMessageDomain(singleMessage, isSender);

                     if (messageList == null)
                     {
                         messageList = new List<MessageDomain>();
                     }

                     messageList.Add(messageToAdd);
                 });
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return messageList;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        protected static MessageDomain BuildMessageDomain(MessageDomain inputMessage, bool isSender)
        {
            var messageDomain = new MessageDomain
            {
                ConversationId = BotConversationId,

                MessageId = inputMessage.MessageId,
                Content = inputMessage.Content,
                CreateDate = inputMessage.CreateDate,
                IsRead = true
            };

            if (isSender)
            {
                // InputMessage.Sender info is human user info
                messageDomain.SenderId = inputMessage.SenderId;
                messageDomain.SenderFullName = inputMessage.SenderFullName;
                messageDomain.SenderUrl = inputMessage.SenderUrl;

                messageDomain.ReceiverId = BotId;
                messageDomain.ReceiverFullName = BotFullName;
                messageDomain.ReceiverUrl = BotUrl;
            }
            else
            {
                messageDomain.SenderId = BotId;
                messageDomain.SenderFullName = BotFullName;
                messageDomain.SenderUrl = BotUrl;

                messageDomain.ReceiverId = inputMessage.SenderId;
                messageDomain.ReceiverFullName = inputMessage.SenderFullName;
                messageDomain.ReceiverUrl = inputMessage.SenderUrl;
            }

            return messageDomain;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static async Task<bool> GetBotResponse(MessageInsertRequest inputMessage)
        {
            bool isSender = (inputMessage.SenderId != null && inputMessage.SenderId != "");
            string humanId = isSender ? inputMessage.SenderId : inputMessage.ReceiverId;

            // Log incoming human message to DB
            InsertMessage(inputMessage);

            // Make sure this wasn't triggered by a bot statement
            if (isSender)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BotAuthKey);

                    // If conversation doesn't already exist, create one.
                    if (ApiConversationId == "")
                    {
                        BotConversationIdResponseDomain _Data = null;
                        var values = new Dictionary<string, string>();
                        values.Add("", "");
                        var content = new FormUrlEncodedContent(values);

                        HttpResponseMessage requestConvo = await client
                            .PostAsync("https://directline.botframework.com/v3/directline/conversations/", content);

                        if (requestConvo.IsSuccessStatusCode)
                        {
                            string jsonResponse = await requestConvo.Content.ReadAsStringAsync();
                            _Data = JsonConvert.DeserializeObject<BotConversationIdResponseDomain>(jsonResponse);
                            ApiConversationId = _Data.ConversationId;
                        }
                    }
                }

                //- Build BotApiMessageRequest
                BotApiMessageRequest message = BuildBotApiMessage(inputMessage, humanId);

                //- Send question to bot. Recieve response.
                using (var client = new HttpClient())
                {
                    string uri = String
                        .Format("https://directline.botframework.com/v3/directline/conversations/{0}/activities", ApiConversationId);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BotAuthKey);

                    //- Send question.
                    string postBody = JsonConvert.SerializeObject(message);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage messageResponse = await client.PostAsync(uri, new StringContent(postBody, Encoding.UTF8, "application/json"));

                    //- If the bot recieved the question, ask for its response.
                    if (messageResponse.IsSuccessStatusCode)
                    {
                        BotApiActivitiesDomain _Activites = null;

                        HttpResponseMessage msg = await client.GetAsync(uri);

                        //- If request successful, log it to the DB and GET OUT!
                        if (msg.IsSuccessStatusCode)
                        {
                            string jsonResponse = await msg.Content.ReadAsStringAsync();
                            _Activites = JsonConvert.DeserializeObject<BotApiActivitiesDomain>(jsonResponse);
                            Activity lastMessage = _Activites.Activities.Last<Activity>();
                            if (lastMessage.Text != null)
                            {
                                var messageToInsert = new MessageInsertRequest
                                {
                                    ReceiverId = humanId,
                                    Content = lastMessage.Text,
                                    SenderId = "",
                                    ConversationId = 0
                                };

                                InsertMessage(messageToInsert);

                                return true;
                            }
                        }
                    }
                }

                //- If you've made it to this point, something has gone wrong.
                var errorMessageToInsert = new MessageInsertRequest
                {
                    ReceiverId = humanId,
                    Content = "Error! Error! Does not compute!",
                    SenderId = "",
                    ConversationId = 0
                };

                InsertMessage(errorMessageToInsert);
            }
            return false;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        protected static BotApiMessageRequest BuildBotApiMessage(MessageInsertRequest inputModel, string humanId)
        {
            var resultMessage = new BotApiMessageRequest();

            // Populate message values.
            resultMessage.Type = "message";
            resultMessage.Text = inputModel.Content;
            resultMessage.From = new Models.Requests.From
            {
                Id = humanId,
                Name = "User"
            };

            return resultMessage;
        }

    }
}
