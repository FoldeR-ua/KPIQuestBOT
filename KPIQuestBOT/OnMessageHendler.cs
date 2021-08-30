using System;
using Telegram.Bot;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace KPIQuestBOT
{
    partial class Dialog
    {
        public static string token = new InitComponents().Token;
        private static TelegramBotClient botClient;
        private static string teamCode;
        private static long teamCodeNum;
        private static bool isWaitingAswear = false;
        private static int questionNum = -1;
        private static int answearNum = 0;
        private static bool start = false;
        private static DataBaseWork work = new DataBaseWork();
        static private List<QuestionAnswear> QA = new List<QuestionAnswear>();
        static private List<string> stickerList = new List<string>();
        static private bool over = false;

        [Obsolete]
        private static void OnMessangeHendler(object sender, MessageEventArgs e)
        {
            long chatId = e.Message.Chat.Id;
            bool logined = false;
            if (e.Message != null && e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && !string.IsNullOrEmpty(e.Message.Text))
            {
                if (over == true)
                    botClient.CloseAsync();

               LoginedClient(e.Message.Text, chatId);

                if (e.Message.Text.Length == 8 && long.TryParse(e.Message.Text, out teamCodeNum) && start)
                    CheckingAccoount(chatId, ref logined, e.Message.Text);


                if (logined && !isWaitingAswear && questionNum == -1)
                    StartQuest(chatId);

                if (logined && !isWaitingAswear && questionNum < QA.Count)
                    ShowFirst(chatId);

                if (questionNum == QA.Count)
                    QuestOver(chatId);
                else
                if (isWaitingAswear && answearNum < QA.Count && QA[answearNum].Answear.Contains(e.Message.Text))
                        CheckingAnswear(chatId);
                
            }
        }

    }
}
