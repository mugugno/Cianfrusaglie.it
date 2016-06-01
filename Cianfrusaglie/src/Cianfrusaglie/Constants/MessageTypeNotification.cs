using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cianfrusaglie.Constants
{
    public enum MessageTypeNotification
    {
        NewMessage, //nuovo messaggio
        NewInterested, //nuovo interessao all'annuncio, per l'annunciatore
        NewChoosed, //nuovo scelto per un annuncio
        NewFeedback, //ricordati di lasciare un feedback
        NewMoreMessages, //nuovi messaggi 
        NewAnotherChoosed, //non è stato scelto l'utente dopo che è stato chiuso l'annuncio
        NewReceivedFeedback //hai ricevuto un feedback

    }


}
