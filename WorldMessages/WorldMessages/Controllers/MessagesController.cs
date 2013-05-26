using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldMessages.DomainModel.Message;
using WorldMessages.DomainModelFramework.Query;

namespace WorldMessages.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IRepository<MessageModel> messagesRepository;

        public MessagesController(IRepository<MessageModel> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }
        //
        // GET: /Messages/
        public ActionResult Messages()
        {
            return View();
        }
    }
}
