using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATTeamE.web.Domain.Convo
{

    public class Message
    {
        [JsonProperty("input")]
        public Input Input { get; set; }

        [JsonProperty("alternate_intents")]
        public bool AlternateIntents { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("entities")]
        public List<Entity> Entities { get; set; }

        [JsonProperty("intents")]
        public List<Intent> Intents { get; set; }

        [JsonProperty("output")]
        public Output Output { get; set; }
    }

    public class Input
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class DialogStack
    {
        [JsonProperty("dialog_node")]
        public string DialogNode { get; set; }
    }

    public class System
    {
        [JsonProperty("dialog_stack")]
        public List<DialogStack> DialogStack { get; set; }
        [JsonProperty("dialog_turn_counter")]
        public int DialogTurnCounter { get; set; }
        [JsonProperty("dialog_request_counter")]
        public int DialogRequestCounter { get; set; }
    }

    public class Context
    {
        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }
        [JsonProperty("system")]
        public System System { get; set; }
    }

    public class Entity
    {
        [JsonProperty("entity")]
        public string _Entity { get; set; }

        [JsonProperty("location")]
        public List<int> Location { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("confidence")]
        public int Confidence { get; set; }
    }

    public class Intent
    {
        [JsonProperty("intent")]
        public string _Intent { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }
    }

    public class LogMessage
    {
        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }

    public class Output
    {
        [JsonProperty("log_messages")]
        public List<LogMessage> LogMessages { get; set; }

        [JsonProperty("text")]
        public List<string> Text { get; set; }

        [JsonProperty("nodes_visited")]
        public List<string> NodesVisited { get; set; }
    }

    

}