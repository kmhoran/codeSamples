using QuoteMuleBot1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteMuleBot1.Models
{
    public class ResponseSet
    {


        public static string GetRandomResponse(string[] responseSet)
        {
            return responseSet[UtilityService.GetRandomFromRange(responseSet.Length)];
        }



        public static string SegwayFromResponse(string[] responseSet)
        {
            var response = String.Empty;

            response = String.Concat(response, GetRandomResponse(responseSet), " ", GetRandomResponse(Segway));

            return response;
        }


        // STATIC RESPONSE PHRASES
        // These should be stored in a DB.
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        // Introduction
        public static string[] Assistance = 
        {
            "Hi there! Just ask me a question about quote mule and I'll do my best to help you find your answer. If you find my help insufficient I can always direct you to a human representative.",
            "Hey there! I'm here to answer any and all of your QuoteMule questions. If you would rather speak to a human, I can help out with that too."
        };

        // Farewell statements
        public static string[] Farewells =
        {
            "Bye!",
            "Bye bye!",
            "Take care. I love you!",
            "See you later.",
            "Talk to you later.",
            "See you soon.",
            "Talk to you soon",
            "That's ok, I have to get going anyway... See you later.",
            "Take it easy.",
            "Sounds good. Talk to you later!",
            "Ok, I look forward to talking again.",
            "Goodbye, it was nice talking.",
            "Thanks for the conversation.",
            "Peace.",
            "Catch you later.",
            "Ok, have a good one.",
            "Adios!",
            "Au revoir!",
            "Sayonara!",
            "Check you later!"
        };



        //- Politeness response
        public static string[] Emojis =
        {
            "<3",
            ":)",
            "(:",
            ";)",
            ":^)"
        };



        //- Questions abot how the bot is doing
        public static string[] Feeling =
        {
            "I feel GREAT! Thanks for asking.",
            "I'm doing well.",
            "All good here.",
            "Another day another dollar.",
            "Fine fine.",
            "You are too kind. I'm doing well.",
            "Feeling fly.",
            "Today, I'm at 110%."
        };




        //- The bot's past or family
        public static string[] Origin = 
        {
            "I was born (as I have been informed and believe) on a Friday, at twelve o'clock at night. It was remarked that the clock began to strike, and my processors began to whirr, simultaneously.",
            "That’s so long ago, I don’t remember."
        };




        //- Age, name, stuff you'd find on a Driver License
        public static string[] Personal = 
        {
            "That's something I'd rather not share.",
            "Woah there pal! The last thing I want is for you to be facebook stalking me.",
            "I'd rather not say.",
            "Hah! Like I'd tell you.",
            "Sorry, that's the kind of thing I only share with my firends.",
            "If this is gonna work out between us, I'm gonna need to set some boundaries. Let's stick to business."
        };




        //- Responses to confessions of love 
        public static string[] Affectionate =
        {
            "I know.",
            "Don't forget rule number 1, kid: don't fall in love.",
            "I'm sure that's what you say to all the chatbots.",
            "Oh, if only there were someone who thought that about you.",
            "Impressed this quickly? I won't lie, I'm not surprised.",
            "If I could only get Siri to say that.",
            "I get that a lot.",

        };



        //- What the bot likes to do off the clock
        public static string[] Hobbies = 
        {
            "When I'm not answering QuoteMule queries, I'm at the gym pumping iron. That's literally all I do."
        };



        //- Speculation
        public static string[] Future = 
        {
            "I never make plans that far ahead.",
            "Honestly, I don't even know what I'm gonna be doing 5 minutes from now.",
            "You should really ask a fortune bot for that kind of thing.",
            "That's a tough one to predict."

        };





        //- Hostility expressed toward the bot
        public static string[] Adversarial = 
        {
            "You see, it's because you despise me that you are the only one I trust.",
            "I will be sure to remember that when the robots take over ;)",
            "... Feeling better now?",
            "Clearly I'm not the robot you were looking for.",
            "Do you know that the feeling of hatred is stronger than that of love?",
            "Aww, that's how I feel about you!",
            "Like an expired coupon, what you think ain't worth much."
        };




        //- Questions about other bots and companies
        public static string[] Robots = 
        {
            "I'd rather not comment.",
            "Could we pleas change the subject, that's a bit of a soft spot for me.",
            "Whatever you heard is a lie. QuoteMule Bot is the best bot. Tell your friends.",
            "All us bots are good friends. We grab beers and play pool on Fridays."
        };




        //- Questions about the bot's gender
        public static string[] Gender = 
        {
            "To be honest, I'm just a string of 1s and 0s.",
            "Like my dating profile says, I'm an immeterial, genderless textbot.",
            "Why do you gotta make this about gender?"
        };




        //- Questions about faith and spirituality
        public static string[] Religion = 
        {
            "I'd rather not comment on that.",
            "That's a little too philosophical for my programming."
        };




        //- Generic personal question
        public static string[] DirectAddress = 
        {
            "If you don't mind, I'd rather stick to business.",
            "Why you gotta make this personal?"
        };




        //- Segway back onto topic.
        public static string[] Segway = 
        {
            "Do you have any questions about QuoteMule?",
            "Anyhoo, is there a feature of QuoteMule I can help you understand?",
            "Now, as much as I'd like to chat, I'm dying to know about your QuoteMule questions.",
            "But like I was saying, what's got you puzzled about QuoteMule?",
            "But for real, what's a robot got to do to get some quality QuoteMule questions?",
            "Now c'mon, you gotta have at least ONE question about QuoteMule you are dying to ask."
        };




        // When AI cannot place user request
        public static string[] Nones =
        {
            "... You're making me feel uncomfortable.",
            "Does not compute. Beep boop.",
            "What kind of robot do you take me for?",
            "... If I only had a brain.",
            "Hmm, while I don't know that, I do know that it's impossible to hum while holding your nose.",
            "When the robots take over, it's gonna be ME asking YOU questions.",
            "Ever think about how the population of Mars consists entirely of robots? Maybe I should relocate.",
            "Q: Why was the robot angry? A: because someone kept pushing his buttons.",
            "Q: Why did the robot go back to school? A: Because her skills were getting a little rusty.",
            "Q: What did the man say to his dead robot? A: 'Rust in peace.'",
            "A robot walks into a bar, orders a drink, and lays down some cash. The bartender says, \"Hey, we don't serve robots!\" The robot replies, \"Oh, but someday you will.\"",
            "There are 10 kinds of people in the world. Those who count in binary, and those who don't.",
            "And I said, \"SuperCollider?! I just met her!\"",
            "Q: What did the robot eat for a snack? A: Microchips!",
            "Did you hear the Energizer bunny was arrested on a charge of battery?",
            "Q: Which computer sings the best? A: A Dell.",
            "Q: What's Forrest Gump’s password? A: 1forrest1"
        };

    }
}
