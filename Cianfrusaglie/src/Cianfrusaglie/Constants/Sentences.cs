using System;
using System.Collections.Generic;
using System.Data;

namespace Cianfrusaglie.Constants
{
    public class Sentences
    {
        public Dictionary<int, List<string>> Sentence = new Dictionary<int, List<string>>()
        {
            { (int) MessageTypeNotification.NewMessage,new List<string>() { "Hai un nuovo messaggio", "/Messages" } },
            { (int) MessageTypeNotification.NewInterested,new List<string>() {"Ci sono nuovi interessati ai tuoi annunci","/History/Index" }},
            { (int) MessageTypeNotification.NewChoosed,new List<string>() { "Sei stato scelto per uno o più annunci", "/History/Index" } },
            { (int) MessageTypeNotification.NewFeedback,new List<string>(){ "Ricordati di inserire i feedback", "/History/Index" }},
            { (int) MessageTypeNotification.NewMoreMessages,new List<string>(){ "Hai nuovi messaggi", "/Messages"} },
            { (int) MessageTypeNotification.NewAnotherChoosed,new List<string>() {"Sono stati chiusi degli annunci", "/History/MyHistory" }},
            { (int) MessageTypeNotification.NewReceivedFeedback,new List<string>() {"Hai ricevuto nuovi feedback", "/Profile" }}
        };
     


}
}
