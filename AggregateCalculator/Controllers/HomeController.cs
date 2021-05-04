using AggregateCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AggregateCalculator.Controllers
{
    public class HomeController : Controller
    {
        List<ResultViewModel> resultRecords = new List<ResultViewModel>();
        List<ScoresRecordViewModel> scoreRecords = new List<ScoresRecordViewModel>();
        public ActionResult Index()
        {
            var model = Session["records"] as List<ResultViewModel>; //Retervie Records From Session
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ScoresRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new ResultViewModel();
                var aggregate = ((model.English + model.LanguageTwo + model.Maths + model.Social + model.Science) * 100) / 500;
                data.Id = Guid.NewGuid();
                model.Id = Guid.NewGuid();
                data.ScoresRecordId = model.Id.ToString();
                data.Aggregate = aggregate;
                if (aggregate >= 70)
                {
                    data.AggregateCategory = AggregateCategoryEnum.Distinction;
                    data.isFail = false;
                }
                else if (aggregate >= 60 && aggregate < 70)
                {
                    data.AggregateCategory = AggregateCategoryEnum.FirstClass;
                    data.isFail = false;
                }
                else if (aggregate >= 50 && aggregate < 60)
                {
                    data.AggregateCategory = AggregateCategoryEnum.SecondClass;
                    data.isFail = false;
                }
                else if (aggregate >= 35 && aggregate < 50)
                {
                    data.AggregateCategory = AggregateCategoryEnum.ThirdClass;
                    data.isFail = false;
                }
                else
                {
                    data.AggregateCategory = AggregateCategoryEnum.Fail;
                    data.isFail = true;
                }

                // To understand the concept of data retervial below please read carefully
                // I am getting the data from session (if any) in a list
                // Than I am adding the new record to the list
                // Than I am adding that list to the session.
                // If there is not data in the session (tempResults == null)
                // In that case I am simply adding the record to the list and then upload it to the session


                var tempResults = Session["records"] as List<ResultViewModel>;
                var tempScores = Session["scores"] as List<ScoresRecordViewModel>;
                if (tempResults == null)
                {
                    resultRecords.Add(data);
                    scoreRecords.Add(model);
                    Session["records"] = resultRecords;
                    Session["scores"] = scoreRecords;
                }
                else
                {
                    tempResults.Add(data);
                    tempScores.Add(model);
                    Session["records"] = tempResults;
                    Session["scores"] = tempScores;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var records = Session["scores"] as List<ScoresRecordViewModel>;
            var model = records.Where(p => p.Id == Guid.Parse(id)).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ScoresRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultRecords = Session["records"] as List<ResultViewModel>;
                var temp = resultRecords.Where(p => p.ScoresRecordId == model.Id.ToString()).FirstOrDefault();
                var data = new ResultViewModel();
                var aggregate = ((model.English + model.LanguageTwo + model.Maths + model.Social + model.Science) * 100) / 500;
                data.Id = temp.Id;
                data.ScoresRecordId = model.Id.ToString();
                data.Aggregate = aggregate;
                if (aggregate >= 70)
                {
                    data.AggregateCategory = AggregateCategoryEnum.Distinction;
                    data.isFail = false;
                }
                else if (aggregate >= 60 && aggregate < 70)
                {
                    data.AggregateCategory = AggregateCategoryEnum.FirstClass;
                    data.isFail = false;
                }
                else if (aggregate >= 50 && aggregate < 60)
                {
                    data.AggregateCategory = AggregateCategoryEnum.SecondClass;
                    data.isFail = false;
                }
                else if (aggregate >= 35 && aggregate < 50)
                {
                    data.AggregateCategory = AggregateCategoryEnum.ThirdClass;
                    data.isFail = false;
                }
                else
                {
                    data.AggregateCategory = AggregateCategoryEnum.Fail;
                    data.isFail = true;
                }

                resultRecords.Remove(temp);
                resultRecords.Add(data);
                Session["records"] = resultRecords;

                var tempScores = Session["scores"] as List<ScoresRecordViewModel>;
                var tempScore = tempScores.Where(p => p.Id == model.Id).FirstOrDefault();
                tempScores.Remove(tempScore);
                tempScores.Add(model);
                Session["scores"] = tempScores;
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}