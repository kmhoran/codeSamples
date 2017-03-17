using QuoteMuleBot1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QuoteMuleBot1.Services
{
    public class DirectAddressService
    {

        public class FaqService
        {

            //....// ===================================================================================

            public static async Task<string> DirectAddressProcessor(Models.Topscoringintent intent)
            {
                Models.Action action = intent.actions[0];

                string response = String.Empty;

                //- For incomplete queries.
                if (action.triggered == false)
                {

                    for (var i = 0; i < action.parameters.Length; i++)
                    {
                        if (action.parameters[i].name == "Subtopic")
                        {
                            string interogativeResponse = "Could you be more specific? What exactly do you want to ";

                            string verbToDo = "know";

                            if (action.parameters[i].value != null)
                            {
                                verbToDo = action.parameters[i].value[0].entity;
                            }

                            response = String.Concat(response, interogativeResponse, verbToDo, "?");

                        }
                    }
                }

                //- For valid queries.
                if (action.triggered == true)
                {
                    //response = "This is a valid FAQ Query!";
                    List<string> topics = new List<string>();
                    List<string> subtopics = new List<string>();

                    response = String.Empty;

                    //- Sort query entities
                    for (var i = 0; i < action.parameters.Length; i++)
                    {

                        if (action.parameters[i].value != null)
                        {
                            if (action.parameters[i].name == "Topic")
                            {
                                for (var j = 0; j < action.parameters[i].value.Length; j++)
                                {
                                    //response = String.Concat(response, action.parameters[i].value[j].entity, ", ");
                                    topics.Add(action.parameters[i].value[j].entity);
                                }
                            }

                            if (action.parameters[i].name == "Subtopic")
                            {
                                //response = String.Concat(response, "CONCERNING: ");
                                for (var k = 0; k < action.parameters[i].value.Length; k++)
                                {
                                    //response = String.Concat(response, action.parameters[i].value[k].entity, " ");
                                    subtopics.Add(action.parameters[i].value[k].entity);
                                }
                            }
                        }
                    }

                    if (subtopics.Count == 0)
                    {
                        topics.Add("definition");
                    }
                    topics.AddRange(subtopics);

                    FaqQueryDomain query = await QuoteMuleService.ParseLuisKeywords(topics);

                    response = query.Content;
                }

                return response;
            }

        }
    }
}