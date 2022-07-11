﻿using marvel.Models;
using Marvel.Api;
using Marvel.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace marvel.Controllers
{
    public class MarvelController : Controller
    {
        private const string APIKEY = "046e7dea92107587b0325bf1148f792b";
        private const string PRIVATEKEY = "b96ff2b51fe33d6f006731902d0afd9bab05291b";
        private MarvelRestClient restClient;

        public MarvelController()
        {
            restClient = new MarvelRestClient(APIKEY, PRIVATEKEY);
        }

        public JsonResult GetAllCharacters(int orderBy)
        {
            var filter = new CharacterRequestFilter();
            filter.Limit = 10;

            filter.OrderBy(Extensions.GetOrderResult(orderBy));

            var listCharacter = new List<CharacterModel>();

            var results = restClient.FindCharacters(filter).Data.Results;

            listCharacter = results.Select(res =>
                        new CharacterModel { Id = res.Id, 
                                             Name = res.Name, 
                                             Description = res.Description, 
                                             Modified = res.Modified, 
                                             UrlDetails = res.Urls.Where(r => r.Type.ToLower() == "detail").Select(r => r.URL).ToList() }).ToList();

            return Json(listCharacter);
        }

        public JsonResult GetCharactersById(string name)
        {
            var listCharacter = new List<CharacterModel>();

            var filter = new CharacterRequestFilter();
            filter.Offset = 0;
            filter.Name = name;

            var resultsCharacters = restClient.FindCharacters(filter).Data.Results;

            listCharacter = resultsCharacters.Select(res =>
                        new CharacterModel
                        {
                            Id = res.Id,
                            Name = res.Name,
                            Description = res.Description,
                            Modified = res.Modified,
                            UrlDetails = res.Urls.Where(r => r.Type.ToLower() == "detail").Select(r => r.URL).ToList(),
                            Series = getSeriesByCharacter(res.Id)
                        }).ToList();

            return Json(listCharacter);
        }

        private List<SeriesModel> getSeriesByCharacter(int id)
        {
            var listSeries = new List<SeriesModel>();

            var filter = new SeriesRequestFilter();
            filter.OrderBy(Extensions.GetOrderResult(0));
            filter.Offset = 0;
            filter.Limit = 50;

            var resultsSeries = restClient.FindCharacterSeries(id.ToString(), filter).Data.Results;

            listSeries = resultsSeries.Select(res =>
                    new SeriesModel
                    {
                        Id = res.Id,
                        Title = res.Title,
                        Description = res.Description,
                        StartYear = res.StartYear,
                        EndYear = res.EndYear,
                        Rating = res.Rating
                    }).ToList();

            return listSeries;
        }
    }
}
