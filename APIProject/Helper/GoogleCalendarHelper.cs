using APIProject.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text;
using APIProject.DTO;

namespace APIProject.Helper
{
    public class GoogleCalendarHelper
    {
        private UserCredential GetGoogleCalendarCredential(string[] scopes)
        {
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "APIP", "APIP.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, scopes, "user", CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }

        public async Task<Event> CreateGoogleCalendar(GoogleCalendarDTO request)
        {
            try {
                string[] scopes = { "https://www.googleapis.com/auth/calendar" };
                string applicationName = "APIProject";

                UserCredential credential = GetGoogleCalendarCredential(scopes);

                // Define services
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });




                // Define request
                Event eventCalendar = new Event
                {
                    //Id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),

                    Summary = request.Summary,
                    Location = request.Location,
                    Start = new EventDateTime
                    {
                        DateTime = request.Start,
                        TimeZone = "Asia/Jakarta",
                    },
                    End = new EventDateTime
                    {
                        DateTime = request.End,
                        TimeZone = "Asia/Jakarta",
                    },
                    Description = request.Description,


                    Attachments = new List<EventAttachment>
                    {
                        new EventAttachment
                        {
                            FileUrl = request.Attachment



                        }

                    }
                };

                string calendarId = "primary"; // Use "primary" for the primary calendar
                var eventRequest = service.Events.Insert(eventCalendar, calendarId);

                var requestCreate = await eventRequest.ExecuteAsync();

                return requestCreate;
            }
            catch (Exception e)
            {
                return new Event();
            }
            
        }




        public async Task<Event> GetGoogleCalendarEvent(string eventId)
        {
            try
            {
                string[] scopes = { "https://www.googleapis.com/auth/calendar.events.readonly" };
                string applicationName = "APIProject";

                UserCredential credential = GetGoogleCalendarCredential(scopes);

                // Initialize the Calendar service
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });

                // Retrieve the event by eventId
                var request = service.Events.Get("primary", eventId);
                var eventItem = await request.ExecuteAsync();
                return eventItem;
            }
            catch (Exception e)
            {
                return new Event();
            }
        
        }

        public List<Event> GetGoogleCalendarEvents()
        {
            try {
                string[] scopes = { "https://www.googleapis.com/auth/calendar.events.readonly" };
                string applicationName = "APIProject";

                UserCredential credential = GetGoogleCalendarCredential(scopes);

                // Initialize the Calendar service
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });

                // Retrieve the event by eventId
                var request = service.Events.List("primary");
                var eventItem = request.Execute().Items;
                return (List<Event>)eventItem;
            }
            catch (Exception e)
            {
                return new List<Event>();
            }
        }
        //create method to search  in a list of events
        public List<Event> GetEventsatDateRange(DateTime start, DateTime end)
        {
            try {
                var eventList = GetGoogleCalendarEvents();
                var events = eventList.Where(x => x.Start.DateTime >= start && x.End.DateTime <= end).ToList();
                return events;
            }
            catch (Exception e)
            {
                return new List<Event>();
            }
            


            //string[] scopes = { "https://www.googleapis.com/auth/calendar.events.readonly" };
            //string applicationName = "APIProject";

            //UserCredential credential = GetGoogleCalendarCredential(scopes);

            //// Initialize the Calendar service
            //var service = new CalendarService(new BaseClientService.Initializer
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = applicationName,
            //});

            //// Retrieve the event by eventId
            //var request = service.Events.List("primary");
            //request.TimeMin = start;
            //request.TimeMax = end;
            //request.ShowDeleted = false;
            //request.SingleEvents = true;
            //request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            //var eventItem = request.Execute().Items;
            //return (List<Event>)eventItem;


        }   
        
        //create method to delete event
        public async Task<bool> DeleteEvent(string eventId)
        {
            try {
                string[] scopes = { "https://www.googleapis.com/auth/calendar.events" };
                string applicationName = "APIProject";

                UserCredential credential = GetGoogleCalendarCredential(scopes);

                // Initialize the Calendar service
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });

                // Retrieve the event by eventId
                var request = service.Events.Delete("primary", eventId);
                await request.ExecuteAsync();
                return true;
            }
            catch (Exception e)
            {   

                return false;
            }
            
        }

        //create method to search by any craitria
        
        public List<Event> SearchEvent(string? search)
        {
            try
            {
                var eventList = GetGoogleCalendarEvents();
                var events = eventList.Where(x => x.Summary.Contains(search) || x.Description.Contains(search) ).ToList();
                return events;
            }
            catch (Exception e)
            {
                return new List<Event>();
            }
            
        }   

    }
}
