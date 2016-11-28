using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

public class TranslationHtmlHelper
{
    private readonly HtmlHelper _htmlHelper;

    public TranslationHtmlHelper(HtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper;
    }

    public JObject GetTranslations()
    {
        var resourceObject = new JObject();

        var resourceSet = Catalogos.Resources.Names.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
        IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();
        while (enumerator.MoveNext())
        {
            resourceObject.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
        }

        return resourceObject;
    }
}

public static class HtmlHelperExtension
{
    public static TranslationHtmlHelper Translation(this HtmlHelper htmlHelper)
    {
        return new TranslationHtmlHelper(htmlHelper);
    }
}