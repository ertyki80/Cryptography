#pragma checksum "C:\Users\Ivan\RiderProjects\Cryptography.Web\Cryptography.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "856f7ea5c57cc71862cbc0d6c28a1d3451647724"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"856f7ea5c57cc71862cbc0d6c28a1d3451647724", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23ac09be4bcfaa7f9829a01d1a134874eaae1f3b", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h3>Select file to encryption</h3>\r\n<br>\r\n");
#nullable restore
#line 3 "C:\Users\Ivan\RiderProjects\Cryptography.Web\Cryptography.Web\Views\Home\Index.cshtml"
 using (Html.BeginForm("AddFile", "Home", FormMethod.Post, new {enctype = "multipart/form-data"}))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""field__wrapper"">
        <input type=""file"" name=""uploadedFile"" id=""field__file-2"" class=""field field__file""/>
        <label class=""field__file-wrapper"" for=""field__file-2"">
            <div class=""field__file-fake"">Upload file</div>
            <input type=""submit"" value=""Proccess"" class=""field__file-button"" />
        </label>
    </div>
");
#nullable restore
#line 12 "C:\Users\Ivan\RiderProjects\Cryptography.Web\Cryptography.Web\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 14 "C:\Users\Ivan\RiderProjects\Cryptography.Web\Cryptography.Web\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("FolderListingComponent"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<script>
</script>

<style>
    input{ 
    outline: none; 
    }
    
    .field__wrapper {
      width: 100%;
      position: relative;
      margin: 15px 0;
      text-align: center;
    }
     
    .field__file {
      opacity: 0;
      visibility: hidden;
      position: absolute;
    }
     
    .field__file-wrapper {
      width: 100%;
      display: -webkit-box;
      display: -ms-flexbox;
      display: flex;
      -webkit-box-pack: justify;
          -ms-flex-pack: justify;
              justify-content: space-between;
      -webkit-box-align: center;
          -ms-flex-align: center;
              align-items: center;
      -ms-flex-wrap: wrap;
          flex-wrap: wrap;
    }
     
    .field__file-fake {
      height: 60px;
      width: calc(100% - 130px);
      display: -webkit-box;
      display: -ms-flexbox;
      display: flex;
      -webkit-box-align: center;
          -ms-flex-align: center;
              align-items: center;
      padding: 0");
            WriteLiteral(@" 15px;
      border: 1px solid #c7c7c7;
      border-radius: 3px 0 0 3px;
      border-right: none;
    }
     
    .field__file-button {
      width: 130px;
      height: 60px;
      background: #1bbc9b;
      color: #fff;
      font-size: 1.125rem;
      font-weight: 700;
      display: -webkit-box;
      display: -ms-flexbox;
      display: flex;
      -webkit-box-align: center;
          -ms-flex-align: center;
              align-items: center;
      -webkit-box-pack: center;
          -ms-flex-pack: center;
              justify-content: center;
      border-radius: 0 3px 3px 0;
      cursor: pointer;
    }
</style>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
