using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace TG_Sample
{
    //https://github.com/sochix/TLSharp#quick-configuration

    //https://blog.holey.cc/2017/08/30/csharp-send-messages-by-telegram-bot/

    public class TGClient
    {
        TelegramClient client;
        int apiId = 1568679;
        string apiHash = "d3a1557238175ceb44037283b7bafa6a";

        public TGClient()
        {
            if(client == null)
            {
                client = new TelegramClient(apiId, apiHash);
            }
            
        }

        public void Run()
        {
            client.ConnectAsync();
        }
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("execute program");

            //Sample();

            SampleScanFriendList();

            //SampleForAfterLogin();

            //SampleForRecieve();

            Console.WriteLine("execute finish");
        }

        private static void SampleForRecieve()
        {
            var apiId = 1568679;
            var apiHash = "d3a1557238175ceb44037283b7bafa6a";

            var client = new TelegramClient(apiId, apiHash);
            client.ConnectAsync().Wait();

            var dialogResult = client.GetUserDialogsAsync();

            dialogResult.Wait();

            var dialog = (TLDialogs)dialogResult.Result;

            foreach(var chat in dialog.Chats)
            {
                var chatObj = ((TLChannel)chat);
                Console.WriteLine($"{chatObj}");
            }

        }

        private static void SampleForAfterLogin()
        {
            var apiId = 1568679;
            var apiHash = "d3a1557238175ceb44037283b7bafa6a";

            var client = new TelegramClient(apiId, apiHash);
            client.ConnectAsync().Wait();

            var result = client.GetContactsAsync();

            var list = result.Result;


            //var me = list.Users.Where(x => x.GetType() == typeof(TLUser) && ((TLUser)x).Username == "SherlockHolmes2020")
            //            .Cast<TLUser>().FirstOrDefault();//@Prof_BlackLotus


            var me = list.Users.Where(x => x.GetType() == typeof(TLUser) && ((TLUser)x).Phone == "886966883479")
                        .Cast<TLUser>().FirstOrDefault();//@Prof_BlackLotus

            client.SendMessageAsync(new TLInputPeerUser() { UserId = me.Id }, "HELLO James, This is Sherlock on TG APP").Wait();
        }

        private static void SampleScanFriendList()
        {
            var apiId = 1568679;
            var apiHash = "d3a1557238175ceb44037283b7bafa6a";

            var client = new TelegramClient(apiId, apiHash);
            client.ConnectAsync().Wait();

            var result = client.GetContactsAsync();

            var list = result.Result;

            var me = list.Users.Where(x => x.GetType() == typeof(TLUser) && ((TLUser)x).Username == "Prof_BlackLotus")
                        .Cast<TLUser>().FirstOrDefault();

            foreach(var user in list.Users)
            {
                var u = ((TLUser)user);
                Console.WriteLine($"Username:{u.Username}, Phone:{u.Phone}");
            }

        }

        private static void Sample()
        {       
            var apiId = 1568679;
            var apiHash = "d3a1557238175ceb44037283b7bafa6a";

            var client = new TelegramClient(apiId, apiHash);
            client.ConnectAsync().Wait();

            var hash = client.SendCodeRequestAsync("+886952003967");

            hash.Wait();

            Console.WriteLine(hash.Result);

            var hashCode = hash.Result;

            var myCode = Console.ReadLine();
 
            var user = client.MakeAuthAsync("+886952003967", hashCode, myCode);

            user.Wait();

            var result = client.GetContactsAsync();

            var list = result.Result;


            var me = list.Users.Where(x => x.GetType() == typeof(TLUser) && ((TLUser)x).Username == "SherlockHolmes2020")
                        .Cast<TLUser>().FirstOrDefault();

            foreach(var member in list.Users)
            {
                Console.WriteLine(((TLUser)member).Username);
            }

            client.SendMessageAsync(new TLInputPeerUser() { UserId = me.Id }, "OUR_MESSAGE").Wait();
        }


        const string Bot_Token = "1317689857:AAFGCwTFe5awvZTGccPvn7raCVzZKFu5dco";

        const string Channel_Numeric_ID = "-1001336397523";


        private static void GetId()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("");
        }


        //1221742323:AAGxDEd8zdzYheCu1jWbqsD2_GWhHtm9Lu4
        //https://api.telegram.org/bot1221742323:AAGxDEd8zdzYheCu1jWbqsD2_GWhHtm9Lu4/sendMessage?chat_id=@seansample&text=Hello+World

        /// <summary>
        /// -1001336397523
        /// 樣板 
        /// https://api.telegram.org/bot1317689857:AAFGCwTFe5awvZTGccPvn7raCVzZKFu5dco/sendMessage?chat_id=@maenchi&text=Hello+World
        /// </summary>
        private static void Test()
        {
            var botClient = new Telegram.Bot.TelegramBotClient(Bot_Token);
            var t = botClient.SendTextMessageAsync(
                chatId: Channel_Numeric_ID,
                text: "Test");

        }
    }
}
