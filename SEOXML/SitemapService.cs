using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOXML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SEOXML
{
    public interface ISitemapService
    {
        /// <summary>
        /// Will remove the Index action off of each controller
        /// </summary>
        bool IgnoreIndexActions { get; set; }
        string GenerateSitemap(Action<List<SitemapItem>> sitemapData = null);
    }

    public class SitemapService : ISitemapService
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly ISitemapGenerator generator;

        public bool IgnoreIndexActions { get; set; } = false;

        public SitemapService(IHttpContextAccessor httpContextAccessor, ISitemapGenerator generator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.generator = generator;
        }

        private List<AssemblyController> GetControllers()
        {
            var name = System.AppDomain.CurrentDomain.FriendlyName;
            var nameSpace = name + ".Controllers";

            var assemType = GetType(nameSpace);

            Assembly asm = Assembly.GetAssembly(assemType);

            return asm.GetTypes()
                    .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any() && m.ReturnType == typeof(IActionResult) || m.ReturnType == typeof(Task<IActionResult>))
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType, Attributes = x.GetCustomAttributes() })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).Select(s => new AssemblyController()
                    {
                        Controller = s.Controller.ToLower().Replace("controller", ""),
                        Action = s.Action,
                        Attributes = s.Attributes,
                        ReturnType = s.ReturnType
                    }).ToList();
        }

        public Type GetType(string nameSpace)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var ass in assembly)
            {
                foreach (var def in ass.DefinedTypes)
                {
                    if (def.Namespace == nameSpace)
                    {
                        return Type.GetType(def.AssemblyQualifiedName);
                    }
                }
            }
            return null;
        }

        public string GenerateSitemap(Action<List<SitemapItem>> sitemapData = null)
        {
            string baseUrl = httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host.Value;

            var sitemapItems = new List<SitemapItem>();

            sitemapData?.Invoke(sitemapItems);

            var controllers = GetControllers();
            foreach (var controller in controllers)
            {
                if (controller.Attributes.Where(s => s.GetType() == typeof(HttpPostAttribute) || s.GetType() == typeof(HttpDeleteAttribute) || s.GetType() == typeof(HttpPutAttribute)).FirstOrDefault() == null)
                {
                    var hideSEO = controller.Attributes.Where(s => s.GetType() == typeof(HideSEOAttribute)).FirstOrDefault();
                    if (hideSEO == null)
                    {
                        string routeUrl = null;
                        SEOAttribute seo = null;

                        var route = controller.Attributes.Where(s => s.GetType() == typeof(RouteAttribute)).FirstOrDefault();
                        if (route != null)
                        {
                            routeUrl = (route as RouteAttribute).Template;
                        }

                        // SEO Settings
                        var seoAttribute = controller.Attributes.Where(s => s.GetType() == typeof(SEOAttribute)).FirstOrDefault();
                        if (seoAttribute != null)
                        {
                            seo = (seoAttribute as SEOAttribute);
                        }
                        else
                        {
                            seo = new SEOAttribute();
                        }

                        if (!String.IsNullOrWhiteSpace(routeUrl))
                        {
                            sitemapItems.Add(new SitemapItem(baseUrl + routeUrl, changeFrequency: seo.Frequency, priority: seo.Priority));
                        }
                        else
                        {
                            if (controller.Controller.ToLower() == "home" && controller.Action.ToLower() == "index")
                            {
                                sitemapItems.Add(new SitemapItem(baseUrl, changeFrequency: seo.Frequency, priority: seo.Priority));
                            }
                            else
                            {
                                if (IgnoreIndexActions && controller.Action.ToLower() == "index")
                                {
                                    sitemapItems.Add(new SitemapItem(baseUrl + "/" + controller.Controller, changeFrequency: seo.Frequency, priority: seo.Priority));
                                }
                                else
                                {
                                    sitemapItems.Add(new SitemapItem(baseUrl + "/" + controller.Controller + "/" + controller.Action, changeFrequency: seo.Frequency, priority: seo.Priority));
                                }
                            }
                        }
                    }
                }
            }


            var sitemap = generator.GenerateSiteMap(sitemapItems);

            return sitemap.ToString();
        }
    }
}