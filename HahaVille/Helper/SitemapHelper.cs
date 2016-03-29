using HahaVille.DAL;
using HahaVille.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace HahaVille.Helper
{
    public static class SitemapHelper
    {
        public static IReadOnlyCollection<SitemapNode> GetSitemapNodes(UrlHelper urlHelper)
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode()
                {
                    Url = urlHelper.AbsoluteContent("/Home"),
                    Priority = 1
                });
            nodes.Add(
               new SitemapNode()
               {
                   Url = urlHelper.AbsoluteContent("/Home/About"),
                   Priority = 0.9
               });
            
             HahaVilleContext db = new HahaVilleContext();
             var listOfGategories = (from c in db.Category
                                     select c.Name.Replace(" ", "-"));
                foreach (var cat in listOfGategories)
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = urlHelper.AbsoluteRouteUrl("Category", new { action = "Category", name = cat }),
                       Frequency = SitemapFrequency.Monthly,
                       Priority = 0.9
                   });
                }

             var objTargetGame = (from g in db.Games
                                  select 
                                      g.Name.Replace(" ", "-")
                                  );
             foreach (var game in objTargetGame)
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = urlHelper.AbsoluteRouteUrl("Game", new { action = "Details", name = game }),
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
                nodes.Add(
                new SitemapNode()
                {
                    Url = urlHelper.AbsoluteRouteUrl("Play", new { action = "Play", name = game }),
                    Frequency = SitemapFrequency.Weekly,
                    Priority = 0.8
                });
            }

            
            nodes.Add(
               new SitemapNode()
               {
                   Url = urlHelper.AbsoluteContent("/Home/Contact"),
                   Priority = 0.7
               });
           

            return nodes;
        }

        public static string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
    }
}