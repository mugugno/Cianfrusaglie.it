using System.Threading.Tasks;

namespace Cianfrusaglie.Services {
    public interface ISmsSender {
        Task SendSmsAsync( string number, string message );
    }
}