using SEOXML.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SEOXML
{
    public interface ISitemapService
    {
        List<SitemapItem> GenerateSitemap(string baseUrl);
        List<AssemblyController> GetControllers();
    }

    public class SitemapService : ISitemapService
    {
        public SitemapService()
        {
            
        }

        public List<AssemblyController> GetControllers()
        {
            var name = System.AppDomain.CurrentDomain.FriendlyName;
            var nameSpace = name + ".Controllers";

            var assemType = GetType(nameSpace);

            Assembly asm = Assembly.GetAssembly(assemType);

            return asm.GetTypes()
                    .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
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

        public List<SitemapItem> GenerateSitemap(string baseUrl)
        {
            var sitemapItems = new List<SitemapItem>();

            var controllers = GetControllers();
            foreach (var controller in controllers)
            {
                sitemapItems.Add(new SitemapItem(baseUrl + "/" + controller.Controller + "/" + controller.Action, changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));
            }

            return sitemapItems;
        }
    }
}