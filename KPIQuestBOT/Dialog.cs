using System;
using Telegram.Bot;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPIQuestBOT
{
    partial class Dialog
    {
        [Obsolete]
        public void StartDialog()
        {
            botClient = new TelegramBotClient(token);
            botClient.OnMessage += OnMessangeHendler;
            botClient.StartReceiving();
            Console.ReadKey();
            botClient.StopReceiving();
        }
        public static void LoginedClient(string message, long chatId)
        {
            if (message == "/login")
            {
                botClient.SendTextMessageAsync(chatId, "Write your team code :");
                start = true;
            }
        }
        public static bool CheckingAccount(long userId, long chatId, ref bool logined, string message)
        {
            teamCode = message;
            work.CheckingData(teamCode, ref logined, userId);
            isWaitingAswear = false;
            if (logined)
            {
                work.ReadJSON(teamCode, ref QA);
                return true;
            }
            else
            {
                botClient.SendTextMessageAsync(chatId, "Uncorrect password . . . ");
                return false;
            }
        }
        public static void StartQuest(long chatId)
        {
            botClient.SendTextMessageAsync(chatId, $" . . . * * * Lets start your game * * * . . .\n            Good luck ");
            botClient.SendStickerAsync(chatId,
                                            sticker: "https://cdn.tlgrm.app/stickers/d06/e20/d06e2057-5c13-324d-b94f-9b5a0e64f2da/256/11.webp");
            questionNum++;
        }
        public static void ShowFirst(long chatId)
        {
            botClient.SendTextMessageAsync(chatId, $"Problem {questionNum + 1} \n\n\n     {QA[questionNum].Question}");
            isWaitingAswear = true;
            questionNum++;
        }
        public static void CheckingAnswear(long chatId)
        {
            botClient.SendTextMessageAsync(chatId, "Answear is right . . .");
            answearNum++;
            botClient.SendTextMessageAsync(chatId, $"Problem {questionNum + 1} \n\n\n     {QA[questionNum].Question}");
            questionNum++;
        }
        public static void QuestOver(long chatId)
        {
            botClient.SendTextMessageAsync(chatId, "Quest is over . . . ");
            botClient.SendStickerAsync(chatId, "https://cdn.tlgrm.app/stickers/696/3ad/6963ad3a-2019-32d2-ac85-34af3c84e50b/256/11.webp");
            over = true;
            botClient.CloseAsync();
        }
    }
   
}
