using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using System.Net;
using Newtonsoft.Json;
using static VACCOVID.CovidNews;

namespace VACCOVID
{
    public class VACCOVID1
    {
        TelegramBotClient botClient = new TelegramBotClient("5596187855:AAGlZfbes7Lf71RgAAILpj6XJqEhh2cRNcE");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerErrorAsync, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($" Бот {botMe.Username} почав працювати ");
            Console.ReadKey();
        }

        private Task HandlerErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в телеграм бот АПІ:\n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
            if (update?.Type == UpdateType.CallbackQuery)
            {
                await HandlerCallbackQuery(botClient, update.CallbackQuery);
            }
        }

        WebClient webClient = new WebClient();




        private async Task HandlerCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery)
        {
            if (callbackQuery.Data.StartsWith("Covid Africa"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: $"Africa");


                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidAfrica");

                var result = JsonConvert.DeserializeObject<List<CovidAfrica>>(json);
                for (int i = 0; i < result.Count; i++)
                {



                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");


                }
                return;
            }
            if (callbackQuery.Data.StartsWith("Covid Place"))
            {


                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Enter the name of the country, enter the name of the country in abbreviated form in capital letters with a space. Example: Ukraine UKR", replyMarkup: new ForceReplyMarkup { Selective = true });

            }
            else
            if (callbackQuery.Message.ReplyToMessage != null && callbackQuery.Message.ReplyToMessage.Text.Contains("Enter the name of the country, enter the name of the country in abbreviated form in capital letters with a space. Example: Ukraine UKR"))
            {
                try
                {
                    string[] country = callbackQuery.Message.Text.Split(' ');

                    var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/Covid/{country[0]}/{country[1]}");
                    var result = JsonConvert.DeserializeObject<List<WorldData>>(json);

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Covid: {result.FirstOrDefault().Continent}\nCountry: {result.FirstOrDefault().Country}\nInfection_Risk: {result.FirstOrDefault().Infection_Risk}\nTotalCases: {result.FirstOrDefault().TotalCases}\nNewCases: {result.FirstOrDefault().NewCases}\nTotalDeaths: {result.FirstOrDefault().TotalCases}\nNewDeaths: {result.FirstOrDefault().NewDeaths}\nTotalRecovered: {result.FirstOrDefault().TotalRecovered}\nNewRecovered: {result.FirstOrDefault().NewRecovered}\nActiveCases: {result.FirstOrDefault().ActiveCases}\nSerious_Critical: {result.FirstOrDefault().Serious_Critical}");
                    return;
                }
                catch
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"invalid form");
                }
            }
            if (callbackQuery.Data.StartsWith("Covid Asian"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Asia");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidAsian");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }
                return;
            }
            if (callbackQuery.Data.StartsWith("Covid Australian and Oceanian counties"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Australian and Oceanian counties");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidAustralian_and_Oceanian_counties");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }
            }
            if (callbackQuery.Data.StartsWith("Covid Country"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Enter the country name in abbreviated form, in lower case, example: ukr", replyMarkup: new ForceReplyMarkup { Selective = true });
            }
            else
            if (callbackQuery.Message.ReplyToMessage != null && callbackQuery.Message.ReplyToMessage.Text.Contains("Enter the country name in abbreviated form, in lower case, example: ukr"))
            {
                string iso = callbackQuery.Message.Text;
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidCountry/{iso}");
                var result = JsonConvert.DeserializeObject<List<CovidCountry>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Name:{result[i].Name}\n " +
                    $" Province:{result[i].Province}\n" +
                    $" iso:{result[i].iso}\n" +
                    $" Confirmed:{result[i].Confirmed}\n" +
                    $" Recovered:{result[i].Recovered}\n" +
                    $" Deaths:{result[i].Deaths}\n" +
                    $" Active:{result[i].Active}\n" +
                     $" Fatality_rate:{result[i].Fatality_rate}\n");
                }
                return;
            }
            if (callbackQuery.Data.StartsWith("Covid European"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"European");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidEuropean");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }
                return;
            }
            if (callbackQuery.Data.StartsWith("Covid Northern American"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $" Northern American");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidNorthernAmerican");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }
                return;
            }
            if (callbackQuery.Data.StartsWith("Covid Southern American"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Southern American");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidSouthernAmerican");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }
                return;
            }
            if (callbackQuery.Data.StartsWith("Treatments"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Treatments");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/Treatments");
                var result = JsonConvert.DeserializeObject<List<Treatments>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $" developerResearcher:{result[i].developerResearcher}\n " +
                    $" trimedName:{result[i].trimedName}\n" +
                    $" category {result[i].category }\n" +
                    $" trimedCategory:{result[i].trimedCategory}\n" +
                    $" phase:{result[i].phase}\n" +
                    $" nextSteps:{result[i].nextSteps}\n" +
                    $" description:{result[i].description}\n" +
                    $" funder:{result[i].funder}\n" +
                    $" FDAApproved:{result[i].FDAApproved}\n" +
                    $" ActiveCases :{result[i].lastUpdated }\n");

                }
            }
            if (callbackQuery.Data.StartsWith("Vaccine"))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Treatments");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/Vaccine");
                var result = JsonConvert.DeserializeObject<List<VaccineWorld>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $" developerResearcher:{result[i].developerResearcher}\n " +
                    $" trimedName:{result[i].trimedName}\n" +
                    $" category:{result[i].category }\n" +
                    $" phase:{result[i].phase}\n" +
                    $" funder:{result[i].funder}\n");

                }
            }

            //await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: $"You choosen: \n{callbackQuery.Data}");
            //return;
        }
        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть команду /keyboard or /inline");
                return;
            }
            else
            if (message.Text == "/inline")
            {
                InlineKeyboardMarkup keyboardMarkup =
                        (
                         new[]
                         {

                              new[]
                              {
                               InlineKeyboardButton.WithCallbackData("Covid Africa", $"Covid Africa"),
                               InlineKeyboardButton.WithCallbackData("Covid Place", $"Covid Place"),
                               InlineKeyboardButton.WithCallbackData("Covid Asian", $"Covid Asian")
                              },
                              new[]
                              {
                                InlineKeyboardButton.WithCallbackData("Covid Australian and Oceanian counties", $"Covid Australian and Oceanian counties"),
                                InlineKeyboardButton.WithCallbackData("Covid Country", $"Covid Country"),
                                InlineKeyboardButton.WithCallbackData("Covid European", $"Covid European")

                              },
                              new[]
                              {
                                InlineKeyboardButton.WithCallbackData("Covid Northern American", $"Covid Northern American"),
                                InlineKeyboardButton.WithCallbackData("Covid Southern American", $"Covid Southern American"),
                                InlineKeyboardButton.WithCallbackData("Treatments", $"Treatments")
                              },
                              new[]
                              {
                                  InlineKeyboardButton.WithCallbackData("Vaccine", $"Vaccine")

                              }

                         }
                        );
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choosen :", replyMarkup: keyboardMarkup);
                return;
            }
            else


            if (message.Text == "/keyboard")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                    new[]
                        {
                        new KeyboardButton [] {  "Covid Africa" },
                        new KeyboardButton [] {  "Covid Place"},
                        new KeyboardButton [] {  "Covid Asian" },
                        new KeyboardButton [] {  "Covid Australian and Oceanian counties" },
                        new KeyboardButton [] {  "Covid Country" },
                        new KeyboardButton [] {  "Covid European" },
                        new KeyboardButton [] {  "Covid Northern American" },
                        new KeyboardButton [] {  "Covid Southern American" },
                        new KeyboardButton [] {  "Covid News" },
                        new KeyboardButton [] {  "Health News" },
                        new KeyboardButton [] {  "Vaccine News" },
                        new KeyboardButton [] {  "Treatments" },
                        new KeyboardButton [] {  "Vaccine" }
                        }
                    )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть пункт меню:", replyMarkup: replyKeyboardMarkup);
                return;
            }
            else

            if (message.Text == "Covid Africa")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Africa");

                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidAfrica");

                var result = JsonConvert.DeserializeObject<List<CovidAfrica>>(json);
                for (int i = 0; i < result.Count; i++)
                {



                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");


                }

            }
            else

            if (message.Text == "Covid Asian")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Asia");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidAsian");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }

            }
            else
            if (message.Text == "Covid Australian and Oceanian counties")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Australian and Oceanian counties");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidAustralian_and_Oceanian_counties");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }
            }
            else
            if (message.Text == "Covid European")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"European");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidEuropean");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }

            }
            else
            if (message.Text == "Covid Northern American")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $" Northern American");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidNorthernAmerican");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }
            }
            else
            if (message.Text == "Covid Southern American")
            {

                await botClient.SendTextMessageAsync(message.Chat.Id, $"Southern American");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidSouthernAmerican");
                var result = JsonConvert.DeserializeObject<List<WorldData>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Country:{result[i].Country}\n " +
                    $" Continent:{result[i].Continent}\n" +
                    $" Infection_Risk:{result[i].Infection_Risk}\n" +
                    $" TotalCases:{result[i].TotalCases}\n" +
                    $" NewCases:{result[i].NewCases}\n" +
                    $" TotalDeaths:{result[i].TotalDeaths}\n" +
                    $" NewDeaths:{result[i].NewDeaths}\n" +
                    $" TotalRecovered:{result[i].TotalRecovered}\n" +
                    $" NewRecovered:{result[i].NewRecovered}\n" +
                    $" ActiveCases :{result[i].ActiveCases }\n" +
                    $" Serious_Critical:{result[i].Serious_Critical}\n");
                }

            }
            else
            if (message.Text == "Covid News")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choosen page 1-25", replyMarkup: new ForceReplyMarkup { Selective = true });
            }
            else
                if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Choosen page 1-25"))
            {

                
                
                    string page = message.Text;
                    var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidNews/{page}");
                    var result = JsonConvert.DeserializeObject<CovidNews>(json);
                     


                    for (int i = 0; i < result.news.Length; i++)
                    {

                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Title: {result.news[i].title}\nnews_id: {result.news[i].news_id}\nlink: { result.news[i].link }\nurlToImage: { result.news[i].urlToImage }\nimageInLocalStorage: {result.news[i].imageInLocalStorage }\nimageFileName{result.news[i].imageFileName }\npubDate: {result.news[i].pubDate }\n content: {result.news[i].content }\nreference: {result.news[i].reference}");

                    }
              


            }
            else
            {
                
            }
            if (message.Text == "Treatments")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Treatments");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/Treatments");
                var result = JsonConvert.DeserializeObject<List<Treatments>>(json);
                for (int i = 0; i < 10; i++)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $" developerResearcher:{result[i].developerResearcher}\n " +
                    $" trimedName:{result[i].trimedName}\n" +
                    $" category {result[i].category }\n" +
                    $" trimedCategory:{result[i].trimedCategory}\n" +
                    $" phase:{result[i].phase}\n" +
                    $" nextSteps:{result[i].nextSteps}\n" +
                    $" description:{result[i].description}\n" +
                    $" funder:{result[i].funder}\n" +
                    $" FDAApproved:{result[i].FDAApproved}\n" +
                    $" ActiveCases :{result[i].lastUpdated }\n");

                }

            }
            else
        if (message.Text == "Vaccine")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Vaccine");
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/Vaccine");
                var result = JsonConvert.DeserializeObject<List<VaccineWorld>>(json);
                for (int i = 0; i < result.Count; i++)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $" developerResearcher:{result[i].developerResearcher}\n " +
                    $" trimedName:{result[i].trimedName}\n" +
                    $" category:{result[i].category }\n" +
                    $" phase:{result[i].phase}\n" +
                    $" funder:{result[i].funder}\n");

                }

            }
            else
            if (message.Text == "Health News")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choosen page 1-25", replyMarkup: new ForceReplyMarkup { Selective = true });
            }
            else
                if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Choosen page 1-25"))
            {
                string page = message.Text;
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/HealthNews/{page}");
                var result = JsonConvert.DeserializeObject<CovidNews>(json);
                for (int i = 0; i < result.news.Length; i++)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Title: {result.news[i].title}\nnews_id: {result.news[i].news_id}\nlink: { result.news[i].link }\nurlToImage: { result.news[i].urlToImage }\nimageInLocalStorage: {result.news[i].imageInLocalStorage }\nimageFileName{result.news[i].imageFileName }\npubDate: {result.news[i].pubDate }\n content: {result.news[i].content }\nreference: {result.news[i].reference}");
                }
                return;

            }
            else
            if (message.Text == "Vaccine News")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choosen page 1-25", replyMarkup: new ForceReplyMarkup { Selective = true });
            }
            else
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Choosen page 1-25"))
            {
                string page = message.Text;
                var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/VaccineNews/{page}");
                var result = JsonConvert.DeserializeObject<CovidNews>(json);
                for (int i = 0; i < result.news.Length; i++)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Title: {result.news[i].title}\nnews_id: {result.news[i].news_id}\nlink: { result.news[i].link }\nurlToImage: { result.news[i].urlToImage }\nimageInLocalStorage: {result.news[i].imageInLocalStorage }\nimageFileName{result.news[i].imageFileName }\npubDate: {result.news[i].pubDate }\n content: {result.news[i].content }\nreference: {result.news[i].reference}");
                }
                return;

            }
            else
              if (message.Text == "Covid Country")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Enter the country name in abbreviated form, in lower case, example: ukr", replyMarkup: new ForceReplyMarkup { Selective = true });
            }
            else
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Enter the country name in abbreviated form, in lower case, example: ukr"))
            {   
                    string iso = message.Text;
                    var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/api/CovidCountry/{iso}");

                    var result = JsonConvert.DeserializeObject<List<CovidCountry>>(json);


                for (int i = 0; i < result.Count; i++)
                {


                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Name:{result[i].Name}\n " +
                    $" Province:{result[i].Province}\n" +
                    $" iso:{result[i].iso}\n" +
                    $" Confirmed:{result[i].Confirmed}\n" +
                    $" Recovered:{result[i].Recovered}\n" +
                    $" Deaths:{result[i].Deaths}\n" +
                    $" Active:{result[i].Active}\n" +
                     $" Fatality_rate:{result[i].Fatality_rate}\n");
                   




                }
                return;



            }
            else


            if (message.Text == "Covid Place")
            {

                await botClient.SendTextMessageAsync(message.Chat.Id, "Enter the name of the country, enter the name of the country in abbreviated form in capital letters with a space. Example: Ukraine UKR", replyMarkup: new ForceReplyMarkup { Selective = true });

            }
            else
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Enter the name of the country, enter the name of the country in abbreviated form in capital letters with a space. Example: Ukraine UKR"))
            {
                try
                {
                    string[] country = message.Text.Split(' ');

                    var json = webClient.DownloadString($"https://apicovidd.herokuapp.com/Covid/{country[0]}/{country[1]}");
                    var result = JsonConvert.DeserializeObject<List<WorldData>>(json);

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Covid: {result.FirstOrDefault().Continent}\nCountry: {result.FirstOrDefault().Country}\nInfection_Risk: {result.FirstOrDefault().Infection_Risk}\nTotalCases: {result.FirstOrDefault().TotalCases}\nNewCases: {result.FirstOrDefault().NewCases}\nTotalDeaths: {result.FirstOrDefault().TotalCases}\nNewDeaths: {result.FirstOrDefault().NewDeaths}\nTotalRecovered: {result.FirstOrDefault().TotalRecovered}\nNewRecovered: {result.FirstOrDefault().NewRecovered}\nActiveCases: {result.FirstOrDefault().ActiveCases}\nSerious_Critical: {result.FirstOrDefault().Serious_Critical}");
                    return;
                }
                catch { await botClient.SendTextMessageAsync(message.Chat.Id, $"invalid form"); }
            }
            //else if(message.Text == "Covid Place")
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, $"invalid form");
            //    return;
            //}









        }
    }
}
