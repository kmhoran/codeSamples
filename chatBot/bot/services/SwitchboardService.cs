using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using QuoteMuleBot1.Domain;
using QuoteMuleBot1.Models;
using QuoteMuleBot1.Services;
using QuoteMuleBot1.Services.Interface;

namespace QuoteMuleBot1
{
    public class SwitchboardService
    {


        public static async System.Threading.Tasks.Task<string> MessageHandler(string inputStr)
        {
            string response = String.Empty;
            
            // Convert input string to FaqLuis Model
            LuisObject faqLuis = await LuisService.ParseFaqInput(inputStr);

            if (faqLuis.topScoringIntent != null)
            {

                switch (faqLuis.topScoringIntent.intent)
                {
                    case "FaqQuery":
                        response = await FaqService.FaqQueryProcessor(faqLuis.topScoringIntent);
                        break;

                    case "Assistance":
                        response = ResponseSet.GetRandomResponse(ResponseSet.Assistance);
                        break;

                    case "DirectAddress":
                        //response = "If you don't mind, I'd rather stick to business right now. Do you have any questions about Quote Mule?";
                        response = await DirectAddressHandler(inputStr);
                        break;

                    case "Compare":
                        //response = //await FAQCompareProcessor(faqLuis.topScoringIntent);
                        break;

                    case "Farewell":
                        response = ResponseSet.GetRandomResponse(ResponseSet.Farewells);
                        break;

                    case "Politeness":
                        response = ResponseSet.GetRandomResponse(ResponseSet.Emojis);
                        break;

                    //- Could not place request
                    default:
                        response = ResponseSet.GetRandomResponse(ResponseSet.Nones);
                        break;
                }

                //response = faqLuis.topScoringIntent.intent;
            }
            return response;
        }




        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static async Task<string> DirectAddressHandler(string inputStr)
        {
            string response = String.Empty;

            // Convert input string to FaqLuis Model
            LuisObject directAddressLuis = await LuisService.ParseDirectAddressInput(inputStr);

            if (directAddressLuis.topScoringIntent != null)
            {

                switch (directAddressLuis.topScoringIntent.intent)
                {
                    //- Questions abot how the bot is doing
                    case "Feeling":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Feeling);
                        break;
                    
                    //- The bot's past or family
                    case "Origin":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Origin);
                        break;

                    //- Age, name, stuff you'd find on a Driver License
                    case "Personal":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Personal);
                        break;

                    //- Responses to confessions of love 
                    case "Affectionate":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Affectionate);
                        break;

                    //- What the bot likes to do off the clock
                    case "Hobbies":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Hobbies);
                        break;

                    //- Speculation
                    case "Future":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Future);
                        break;

                    //- Hostility expressed toward the bot
                    case "Adversarial":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Adversarial);
                        break;

                    //- Questions about other bots and companies
                    case "Robots":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Robots);
                        break;

                    //- Questions bout the bot's gender
                    case "Gender":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Gender);
                        break;

                    //- Questions about faith and spirituality
                    case "Religion":
                        response = ResponseSet.SegwayFromResponse(ResponseSet.Religion);
                        break;

                    //- Could not place
                    default:
                        response = ResponseSet.SegwayFromResponse(ResponseSet.DirectAddress);
                        break;
                }
            }
            return response;
        }
    }
}