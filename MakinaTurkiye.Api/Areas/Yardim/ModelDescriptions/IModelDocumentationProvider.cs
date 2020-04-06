using System;
using System.Reflection;

namespace MakinaTurkiye.Api.Areas.Yardim.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}