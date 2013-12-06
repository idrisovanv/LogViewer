using System;
using System.Web.Mvc;

namespace LogViewer.Controllers
{
    using System.Data;
    using System.Reflection;

    using log4net;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Threading;

    public class LogController : Controller
    {               
        Action<ILog, string>[] actions = new Action<ILog, string>[] {
            (l, s) => l.Info(s),
            (l, s) => l.Debug(s),
            (l, s) => l.Warn(s),
            (l, s) => l.Error(s),        
        };

        #region logger random namers
        string[] names = new[] {             
            "Natasha", 
            "Andrey", 
            "Test", 
            "Hello", 
            "World", 
            "ClassName",
            "Driver",
            "Controller",
            "System",
            "Microsoft",
            "Love",
            "Hate",
            "Loggers",
            "Super",
            "Task",
            "Dell",
            "Internet",
            "Crazy",
            "Beer",
            "Cold",
            "Warm",
            "Stupid",
            "Errors",
            "Exceptions",
        };
        #endregion

        /// <summary>
        /// Объект log4net для логгирования ошибок
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Log/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogInfo()
        {
            Logger.Info("This is \"Info\" level message");
            return this.RedirectToAction("Index");
        }

        public ActionResult LogDebug()
        {
            Logger.Debug("This is \"Debug\" level message");
            return this.RedirectToAction("Index");
        }

        public ActionResult LogWarn()
        {
            Logger.Warn("This is \"Warn\" level message");
            return this.RedirectToAction("Index");
        }

        public ActionResult LogError()
        {
            Logger.Error("This is \"Error\" level message");
            return this.RedirectToAction("Index");
        }

        public ActionResult LogErrorException()
        {
            Logger.Error("This is \"Error Exception\" level message", new Exception("This is Exception"));
            return this.RedirectToAction("Index");
        }

        public ActionResult LogThrowException()
        {
            try
            {
                this.TestExceptionLevel1();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
            throw new NotImplementedException("никогда не вызовется");
        }

        private void TestExceptionLevel1()
        {
            this.TestExceptionLevel2();
        }

        private void TestExceptionLevel2()
        {
            this.TestExceptionLevel3();
        }

        private void TestExceptionLevel3()
        {
            this.TestExceptionLevelFinal();
        }

        private void TestExceptionLevelFinal()
        {
            throw new RowNotInTableException("This is test Exception");
        }

        public void Auto()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Rand();
                    Thread.Sleep(1000);
                }
            });
        }

        public void Rand()
        {            
            var rand = new Random();
            var length = rand.Next(Math.Min(3, names.Length / 2), names.Length);           

            var localNames = new List<string>();
            for (int i = 0; i < length; i++)
            {
                localNames.Add(names[rand.Next(names.Length)]);
            }

            var resultName = string.Join(".", localNames);
            
            var localLogger = LogManager.GetLogger(resultName);

            actions[rand.Next(actions.Length)](localLogger, "This is Random message " + resultName);
            
        }

    }
}
