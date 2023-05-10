using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Apis;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Calendar.v3.Data;
using RestSharp;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Google.Apis.Calendar.v3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace TestServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoogleAuth : ControllerBase
    {
        MyContext context;
        public GoogleAuth(MyContext context)
        {
            this.context = context;
        }
        private static Random random = new Random();
        static string randomToken()
        {
            
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        async Task<bool> InsertEvents(string accessToken, string Id, string calendarId)
        {
            var options = new RestClientOptions("https://www.googleapis.com/calendar/v3/calendars")
            {
                Authenticator = new RestSharp.Authenticators.OAuth2.OAuth2AuthorizationRequestHeaderAuthenticator(accessToken),
            };
            var client = new RestClient(options);            
            var req = new RestRequest($"{calendarId}/events");
            req.AddQueryParameter("key", "AIzaSyB8MCVKFHWdUMAFCmnavND0jOZ-Fyl8kQk");
            var respPromise = client.ExecuteAsync<Google.Apis.Calendar.v3.Data.Events>(req);
            var allSections = context.Teaches_.Where(s => s.instructor_id == Id).Include(s => s.section).Include(s=>s.section).ThenInclude(s => s.Event).ThenInclude(s => s.occurences).Include(s=>s.section).ThenInclude(s => s.course).Select(s => s.section).ToList();
            var resp = await respPromise;
            if (!resp.IsSuccessful) return false;
            List<Google.Apis.Calendar.v3.Data.Event> EventList = resp.Data.Items.ToList();
            Dictionary<int, List<int>> dic = new();
            foreach (var itr in EventList)
            {
                string Desc = itr.Description;
                if (Desc == "" || !Desc.Contains('|')|| !Desc.Split('|')[0].Contains('-'))
                {
                    var delreq = new RestRequest($"{calendarId}/events/{itr.Id}", Method.Delete);
                    delreq.AddQueryParameter("key", "AIzaSyB8MCVKFHWdUMAFCmnavND0jOZ-Fyl8kQk");
                    await client.ExecuteAsync(delreq);
                    continue;
                }
                var ident = Desc.Split('|')[0].Split('-');
                if (allSections.Where(s => s.event_id == Convert.ToInt32(ident[0]) && s.Event.occurences.Where(t => t.occurence_id == Convert.ToInt32(ident[1])).Count()>0).Count() > 0)
                {
                    if (dic.ContainsKey(Convert.ToInt32(ident[0])))
                    {
                        dic[Convert.ToInt32(ident[0])].Add(Convert.ToInt32(ident[1]));
                    }
                    else
                    {
                        dic.Add(Convert.ToInt32(ident[0]), new List<int>());
                        dic[Convert.ToInt32(ident[0])].Add(Convert.ToInt32(ident[1]));
                    }
                }
                else
                {
                    var delreq = new RestRequest($"{calendarId}/events/{itr.Id}", Method.Delete);
                    delreq.AddQueryParameter("key", "AIzaSyB8MCVKFHWdUMAFCmnavND0jOZ-Fyl8kQk");
                    await client.ExecuteAsync(delreq);
                    continue;
                }
            }
            foreach (var itr_ in allSections)
            {
                foreach(var itr in itr_.Event.occurences)
                {
                    if (dic.ContainsKey(itr_.event_id) && dic[itr_.event_id].Contains(itr.occurence_id)) continue;
                    GoogleEventDTO newEv = new GoogleEventDTO()
                    {
                        description = $"{itr.event_id}-{itr.occurence_id}|",
                        summary = itr_.course.course_name,
                        location = itr.room_code != null ? itr.room_code : "",
                        recurrence = new String[] {$"RRULE:FREQ=WEEKLY;UNTIL={new DateTime(itr.date_end.Year, itr.date_end.Month, itr.date_end.Day).ToString("yyyyMMdd")}T235959Z"},                                                
                    };
                    GoogleDateTime strtTime = new();
                    GoogleDateTime endTime = new();
                    //strtTime.DateTime = new DateTime(itr.date_start.Year, itr.date_start.Month, itr.date_start.Day, itr.time_begin.Hours, itr.time_begin.Minutes, itr.time_begin.Seconds);
                    strtTime.timeZone = "Asia/Kolkata";
                    //endTime.DateTime = new DateTime(itr.date_start.Year, itr.date_start.Month, itr.date_start.Day, itr.time_end.Hours, itr.time_end.Minutes, itr.time_end.Seconds);
                    endTime.timeZone = "Asia/Kolkata";
                    while ((int)itr.date_start.DayOfWeek != itr.day)
                    {
                        itr.date_start = itr.date_start.AddDays(1);
                    }
                    strtTime.dateTime = new DateTime(itr.date_start.Year, itr.date_start.Month, itr.date_start.Day, itr.time_begin.Hours, itr.time_begin.Minutes, itr.time_begin.Seconds).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
                    endTime.dateTime = new DateTime(itr.date_start.Year, itr.date_start.Month, itr.date_start.Day, itr.time_end.Hours, itr.time_end.Minutes, itr.time_end.Seconds).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
                    newEv.start = strtTime;
                    newEv.end = endTime;                    
                    var insreq = new RestRequest($"{calendarId}/events", Method.Post);
                    insreq.AddQueryParameter("key", "AIzaSyB8MCVKFHWdUMAFCmnavND0jOZ-Fyl8kQk");
                    string json = JsonConvert.SerializeObject(newEv);
                    insreq.AddJsonBody(json, false);                    
                    var zresp = await client.ExecuteAsync(insreq);
                    int x = 8;
                }
            }

            return true;
        }
        [HttpGet("callogin")]
        [Authorize]
        public async Task<IActionResult> LoginAuth()
        {
            string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return Unauthorized();
            }
            Calendar? usrCalData = context.Calendar.SingleOrDefault(s => s.instructor_id == id);
            if (usrCalData != null)
            {
                //var options = new RestClientOptions("https://oauth2.googleapis.com/");                
                //var client = new RestClient(options);
                //var req = new RestRequest("/token", Method.Post);
                //req.AddHeader("content-type", "application/x-www-form-urlencoded");
                //req.AddParameter("application/x-www-form-urlencoded", $"client_id=355872611957-di0n16scktb12ccurucagoob58c39e8a.apps.googleusercontent.com&client_secret=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&refresh_token={usrCalData.refreshToken}&grant_type=refresh_token");
                //var resp = client.Execute<refreshResp>(req);
                //if (!resp.IsSuccessful)
                //{
                //    return BadRequest();
                //}
                //string accessToken = resp.Data.access_token;
                //await InsertEvents(accessToken, id, usrCalData.calName);
                //return Ok("Synced");
                ////context.Calendar.Remove(usrCalData);
                ////context.SaveChanges();
            }
            var alreadyToken = context.GoogleMiddleTokens.SingleOrDefault(s => s.instructor_id == id);
            if (alreadyToken != null)
            {
                context.GoogleMiddleTokens.Remove(alreadyToken);
                context.SaveChanges();
            }
            var midtoken = randomToken();
            var tokenEntry = new GoogleMiddleToken()
            {
                token = midtoken,
                instructor_id = id
            };
            context.Add<GoogleMiddleToken>(tokenEntry);
            context.SaveChanges();
            var oauthClient = new Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow(new Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets()
                {
                    ClientId = "355872611957-di0n16scktb12ccurucagoob58c39e8a.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new string[]
                {
                    "profile",
                    "https://www.googleapis.com/auth/calendar"
                },               
            });
            var oauthObj =  oauthClient.CreateAuthorizationCodeRequest("http://localhost:5143/googleauth/calredir");
            oauthObj.State = midtoken;
            
            //Google.Apis.Auth.OAuth2
           // Google.Apis.Auth.OAuth2.Requests.AuthorizationCodeTokenRequest.
            return Ok(oauthObj.Build().ToString());
        }
        [HttpGet("calredir")]
        public async Task<IActionResult> RedirAuth([FromQuery]string? error, [FromQuery]string? code, [FromQuery]string? state) {
            if (error != null) { 
                return BadRequest(error);
            }
            if (code == null || state == null)
            {
                return BadRequest(error);
            }
            GoogleMiddleToken? midtoken = context.GoogleMiddleTokens.SingleOrDefault(s => s.token == state);
            if (midtoken == null)
            {
                return Unauthorized("No request found");
            }
            string id = midtoken.instructor_id;
            var oauthClient = new Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow(new Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets()
                {
                    ClientId = "355872611957-di0n16scktb12ccurucagoob58c39e8a.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new string[]
                {
                    "profile",
                    "https://www.googleapis.com/auth/calendar"
                }
            });
            var token = await oauthClient.ExchangeCodeForTokenAsync("1", code, "http://localhost:5143/googleauth/calredir", CancellationToken.None);            
            var options = new RestClientOptions("https://www.googleapis.com/calendar/v3/")
            {
                Authenticator = new RestSharp.Authenticators.OAuth2.OAuth2AuthorizationRequestHeaderAuthenticator(token.AccessToken),                
            };
            Calendar? usrCalData = context.Calendar.SingleOrDefault(s => s.instructor_id == id);
            if (usrCalData != null)
            {
                await InsertEvents(token.AccessToken, id, usrCalData.calName);
                return Ok("Synced");
            }
            Google.Apis.Calendar.v3.Data.Calendar newCal = new Google.Apis.Calendar.v3.Data.Calendar()
            {
                Summary = "IIT Jammu TTA",
                TimeZone = "Asia/Kolkata"
            };
            var client = new RestClient(options);
            var addCal = new RestRequest("/calendars", Method.Post);
            addCal.AddQueryParameter("key", "AIzaSyB8MCVKFHWdUMAFCmnavND0jOZ-Fyl8kQk");
            addCal.AddJsonBody(newCal);
            var resp = await client.ExecuteAsync<Google.Apis.Calendar.v3.Data.Calendar>(addCal);
            if (!resp.IsSuccessStatusCode || resp.Data == null) return BadRequest();
            newCal = resp.Data;
            var calObj = new Calendar()
            {
                instructor_id = id,
                calName = newCal.Id,
                refreshToken = token.RefreshToken,
                accessToken = token.AccessToken
            };
            context.Add<Calendar>(calObj);
            context.SaveChanges();
            await InsertEvents(token.AccessToken, id, newCal.Id);
            return Ok("Synced");
        }
        [HttpGet("trst")]
        public async Task<IActionResult> Trst()
        {
            await InsertEvents("ya29.a0AWY7CknX9UmUXZ5DzqvIoL2LOh_QMtftQCTZXvjY3tUg_yX7nWUWn8LgV_QrFxRENaPkwCitwVocnTdj9wGK9hs7ISyhkrypRbnFQ6N1sCzwNHHxcn-dIw_6fFQl1N9EKKnUe0BwXeDPhIsS371WULh2YVB7mQaCgYKATESARMSFQG1tDrpQMIynYQnVo64Tv63qtM26w0165",
                "IITJMU11059", "9j1ieap8d7s7cop86o9cujlvr8@group.calendar.google.com");
            return Ok();
        }
    }
    class refreshResp
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
    }
    class GoogleEventDTO
    {
        public string description { get; set; }
        public string location { get; set; }
        public string summary { get; set; }
        public string[] recurrence { get; set; }
        public GoogleDateTime end { get; set; }
        public GoogleDateTime start { get; set; }
    }
    class GoogleDateTime
    {
        public string dateTime { get; set; }
        public string timeZone { get; set; }
    }
}
