using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WikiWebStarter.Web.Models.Responses;

namespace ATTeamE.web.Controllers.API
{
    [RoutePrefix("api/speech-to-text/token")]
    public class SpeechToTextExApiController : ApiController
    {
        [Route(""), HttpGet]
        public HttpResponseMessage ConsoleSpeechToText()
        {
            SuccessResponse response = new SuccessResponse();

            return Request.CreateResponse(response);
        }

    }
}
