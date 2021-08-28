using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace KPIQuestBOT
{
    class Program
    {
        private static string token = "1956989663:AAEIVfY41UuaVz452u_jgs071TqR3DwNqe0";
        private static TelegramBotClient botClient;
        public static string teamCode;
        public static long teamCodeNum;
        public static bool turn = false;
        public static bool logined = false;
        public static bool isWaitingAswear = false;
        public static int questionNum = -1;
        public static int answearNum = 0;
        static public DataBaseWork work = new DataBaseWork();
        static public List<QuestionAnswear> QA = new List<QuestionAnswear>();
        static public List<string> stickerList = new List<string>();

        [Obsolete]
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient(token);
            botClient.OnMessage += OnMessangeHendler;
            botClient.StartReceiving();
            Console.ReadKey();
            botClient.StopReceiving();
        }

        [Obsolete]
        private static async void OnMessangeHendler(object sender, MessageEventArgs e)
        {
            long chatId = e.Message.Chat.Id;
            string login1 = $"Write tour team-code:"; 
            if (e.Message != null && e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && !string.IsNullOrEmpty(e.Message.Text))
            {
              
                if (e.Message.Text == "/login")
                {
                    await botClient.SendTextMessageAsync(chatId, "Write your team code :");
                    turn = true;
                }
                   
                if (e.Message.Text.Length == 8 && long.TryParse(e.Message.Text, out teamCodeNum) && turn )
                { 
                    teamCode = e.Message.Text;
                    work.CheckingData(teamCode, ref logined);
                    isWaitingAswear = false;
                    if (logined)
                        work.ReadJSON(teamCode, ref QA);
                    else await botClient.SendTextMessageAsync(chatId, "Uncorrect password . . . ");
                }
                if (logined && !isWaitingAswear && questionNum == -1)
                {
                    await botClient.SendTextMessageAsync(chatId, $" . . . * * * Lets start your game * * * . . .\n            Good luck ");
                    await botClient.SendStickerAsync(chatId,
                                                    sticker: "https://cdn.tlgrm.app/stickers/d06/e20/d06e2057-5c13-324d-b94f-9b5a0e64f2da/256/11.webp");

                    questionNum++;
                }
               
                if (logined && !isWaitingAswear && questionNum < QA.Count) 
                {
                    await botClient.SendTextMessageAsync(chatId, QA[questionNum].Question);
                    isWaitingAswear = true;
                    questionNum++;
                }
                try
                {
                    if (isWaitingAswear && answearNum < QA.Count && QA[answearNum].Answear.Contains(e.Message.Text))
                    {
                        await botClient.SendTextMessageAsync(chatId, "Answear is right . . .");
                        answearNum++;
                        await botClient.SendTextMessageAsync(chatId, QA[questionNum].Question);
                        questionNum++; 
                    }
                }
                catch 
                {
                    await botClient.SendTextMessageAsync(chatId, "Quest is over . . .");
                    if (questionNum > QA.Count - 1)
                        await botClient.SendStickerAsync(chatId, "https://cdn.tlgrm.app/stickers/696/3ad/6963ad3a-2019-32d2-ac85-34af3c84e50b/256/11.webp");
                }
            }
        }
    }
}

