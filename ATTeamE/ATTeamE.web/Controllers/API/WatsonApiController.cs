using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATTeamE.web.Interfaces;
using ATTeamE.web.Domain.Convo;
using ATTeamE.web.Models.Responses;
using ATTeamE.web.App_Start;
using ATTeamE.web.Services.Watson;
using System.Net.Http.Headers;

namespace ATTeamE.web.Controllers.API
{
    [RoutePrefix("api")]
    public class WatsonApiController : ApiController
    {
        private IWatsonService _watsonService;

        public WatsonApiController(IWatsonService watsonInject)
        {
            _watsonService = watsonInject;
        }

        [Route("convo/response")]
        public HttpResponseMessage GetResponseMessage(string request)
        {
            Message msg = _watsonService.GetResponseMessage(request);
            ItemResponse<Message> response = new ItemResponse<Message>(msg);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("token/{apiName}")]
        [HttpGet]
        public HttpResponseMessage GetToken(string apiName)
        {
            _watsonService.GetToken(apiName);
            SuccessResponse response = new SuccessResponse();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("speech"), HttpGet]
        public HttpResponseMessage GetTextToSpeech(string botReply)
        {

            try
            {
                string speech = botReply;
                var text = _watsonService.ConvertTextToSpeech(speech);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(text);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "Sabio";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/wav");

                return response;

            }
            catch
            {
                return null;

            }
        }

        //GET api/<controller>
        public void ConvertSpeechToText()
        {
            //try
            //{
            //    // var results = _watsonService.ConvertSpeechToText();
            //    // string url = "https://watson-api-explorer.mybluemix.net/text-to-speech/api/v1/session?" + 
            //    // string url = "https://stream.watsonplatform.net/speech-to-text/api/v1/recognize?"
            //}

            //catch (Exception ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //}
            //return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
