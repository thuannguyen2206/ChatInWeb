#pragma checksum "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "da86678829e75f80f375d9ac0b53523b9ad90263"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PreviewGroup), @"mvc.1.0.view", @"/Views/Shared/_PreviewGroup.cshtml")]
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
#nullable restore
#line 1 "D:\Document\Repos\WebChat\WebChat\Views\_ViewImports.cshtml"
using WebChat;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Document\Repos\WebChat\WebChat\Views\_ViewImports.cshtml"
using WebChat.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da86678829e75f80f375d9ac0b53523b9ad90263", @"/Views/Shared/_PreviewGroup.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a814993b2539efb9958d30a9277d5b3fc4f5208f", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PreviewGroup : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebChat.Common.ViewModels.Discussion.DiscussionDetailViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "file", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("input-avatar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("accept", new global::Microsoft.AspNetCore.Html.HtmlString(".jpg, .png, .gif, .jpeg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("group-name"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100%;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("select-members"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateDiscussion", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Discussion", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""modal-dialog modal-lg"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
    <div class=""modal-content"">
        <div class=""modal-body"">
            <button type=""button"" class=""close"" data-dismiss=""modal"" title=""Close""><span aria-hidden=""true"">&times;</span></button>
            <div class=""group-info"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "da86678829e75f80f375d9ac0b53523b9ad902638198", async() => {
                WriteLiteral("\r\n                    <div class=\"form-group\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "da86678829e75f80f375d9ac0b53523b9ad902638530", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 10 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.GroupId);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                    </div>
                    <div class=""row"">
                        <div class=""group-detail col-md-7"">
                            <div class=""avatar-group"">
                                <img class=""avatar-xl"" id=""avatar-upload-preview""");
                BeginWriteAttribute("src", " src=\"", 904, "\"", 928, 1);
#nullable restore
#line 15 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
WriteAttributeValue("", 910, Model.GroupAvatar, 910, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"avatar\">\r\n                                <label class=\"badge\" title=\"Ch???nh s???a\">\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "da86678829e75f80f375d9ac0b53523b9ad9026311313", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 17 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.AvatarFile);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    <span class=\"material-icons\">edit</span>\r\n                                </label>\r\n                            </div>\r\n                            <h6>");
#nullable restore
#line 21 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                           Write(Model.TotalMember);

#line default
#line hidden
#nullable disable
                WriteLiteral(" th??nh vi??n</h6>\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "da86678829e75f80f375d9ac0b53523b9ad9026313664", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 22 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.GroupName);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </div>\r\n                        <div class=\"invite-member col-md-5\">\r\n                            <label>Th??m th??nh vi??n cho nh??m:</label>\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "da86678829e75f80f375d9ac0b53523b9ad9026315644", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("multiple", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
#nullable restore
#line 26 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ListInviteMemberIds);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-outline-light\" id=\"btn-edit-group\">L??u thay ?????i</button>\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <div class=\"group-members\">\r\n                    <p>Th??nh vi??n c???a nh??m:</p>\r\n                    <table class=\"table table-hover\">\r\n                        <tbody>\r\n");
#nullable restore
#line 35 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                             foreach (var item in Model.ListMembers)
                            {
                                var isOwner = Model.OwnerId == item.Id ? " (Owner)" : "";

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td><img class=\"avatar-md\"");
            BeginWriteAttribute("src", " src=\"", 2462, "\"", 2484, 1);
#nullable restore
#line 39 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
WriteAttributeValue("", 2468, item.AvatarLink, 2468, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 2485, "\"", 2507, 1);
#nullable restore
#line 39 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
WriteAttributeValue("", 2493, item.Username, 2493, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"avatar\" /></td>\r\n                                    <td class=\"member-name\"");
            BeginWriteAttribute("title", " title=\"", 2590, "\"", 2611, 1);
#nullable restore
#line 40 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
WriteAttributeValue("", 2598, item.Address, 2598, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 40 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                                                             Write(item.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral("<b>");
#nullable restore
#line 40 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                                                                              Write(isOwner);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n                                    <td>\r\n");
#nullable restore
#line 42 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                         if (!item.IsFriend)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <a href=\"#\" data-contactid=\"");
#nullable restore
#line 44 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                                                   Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"btn btn-primary btn-add-friend\">K???t b???n</a>\r\n");
#nullable restore
#line 45 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </td>\r\n                                    <td>\r\n");
#nullable restore
#line 48 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                         if (Model.UserId == Model.OwnerId && item.Id != Model.OwnerId)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <a href=\"#\" data-memberid=\"");
#nullable restore
#line 50 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                                                  Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"btn btn-danger btn-delete\">X??a kh???i nh??m</a>\r\n");
#nullable restore
#line 51 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 54 "D:\Document\Repos\WebChat\WebChat\Views\Shared\_PreviewGroup.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

<script>
    $(document).ready(function () {
        // Preview upload image
        $('#input-avatar').off(""change"").on(""change"", function () {
            let input = this;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#avatar-upload-preview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            };
        });

        // select2 to get list users are not this group member
        let groupId = $('.hidden-field #chatGroupId').val();
        $(""#select-members"").select2({
            language: 'vi',
            placeholder: 'T??m th??nh vi??n m???i',
            ajax: {
                url: '/Discussion/GetListToAddMember',
                dataType: 'json',
             ");
            WriteLiteral(@"   delay: 250,
                data: function (params) {
                    return {
                        keyword: params.term,
                        page: params.page || 1,
                        groupId: groupId
                    };
                },
                processResults: function (data, page) {
                    return {
                        pagination: {
                            more: (data.length > 0) ? true : false
                        },
                        results: $.map(data, function (item) {
                            return {
                                id: item.contactId,
                                text: item.userName,
                                html: '<div><img src=""' + item.avatarLink + '"" class=""avatar-md"" /> ' + item.userName + '</div>',
                            };
                        })
                    };
                }
            },
            templateResult: function (data) {
                return da");
            WriteLiteral(@"ta.html;
            },
            escapeMarkup: function (m) {
                return m;
            }
        });

        $("".group-members"").on(""click"", "".btn-add-friend"", function () {
            let contactId = $(this).data('contactid');
            AddContact(contactId, """"); //new-contact.js
        });

        $("".group-members"").on(""click"", "".btn-delete"", function () {
            let item = $(this);
            let memberId = item.data('memberid');
            $.ajax({
                type: ""POST"",
                url: ""/Discussion/RemoveMember"",
                data: { groupId: groupId, memberId: memberId },
                dataType: ""json"",
                success: function (res) {
                    let getMember = $("".group-detail h6"").text().split("" "")[0];
                    let setMember = parseInt(getMember) - 1;
                    if (res === true) {
                        item.closest(""tr"").remove();
                        $("".group-detail h6"").text(setMembe");
            WriteLiteral(@"r + "" th??nh vi??n"");
                    }
                }
            });
        });

        let memberPageIndex = 1;
        let isLoadMember = true;
        $('.group-members').scroll(function () {
            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight - 2) {
                if (isLoadMember) {
                    memberPageIndex += 1;
                    LoadMember(memberPageIndex);
                }
            }
        });

        function LoadMember(current) {
            $.ajax({
                type: ""GET"",
                url: ""/Discussion/LoadMembers"",
                data: { groupId: groupId, pageIndex: current },
                dataType: ""json"",
                success: function (data) {
                    if (data.status === true) {
                        let html = '', btnDel = '', btnAddFriend = '';
                        $(data.result.listMembers).each(function (index, item) {
                            if (!item.isFriend) {");
            WriteLiteral(@"
                                btnAddFriend = `<a href=""#"" data-contactid=""${item.id}"" class=""btn btn-primary btn-add-friend"">K???t b???n</a>`;
                            }
                            if (data.result.userId === data.result.ownerId && item.id !== data.result.ownerId)
                            {
                                btnDel = `<a href=""#"" data-memberid=""${item.id}"" class=""btn btn-danger btn-delete"">X??a kh???i nh??m</a>`;
                            }
                            html += `<tr>
                                        <td><img class=""avatar-md"" src=""${item.avatarLink}"" title=""${item.username}"" alt=""avatar""></td>
                                        <td class=""member-name"" title=""${item.address}"">${item.username}<b>${data.result.ownerId == item.id ? "" (Owner)"" : """"}</b></td>
                                        <td>${btnAddFriend}</td>
                                        <td>${btnDel}</td>
                                     </tr>`;
                  ");
            WriteLiteral(@"          btnDel = ''; btnAddFriend = '';
                        });
                        $('.group-members .table tbody').append(html);
                    } else {
                        isLoadMember = false;
                        memberPageIndex -= 1;
                    }
                }
            });
        }
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebChat.Common.ViewModels.Discussion.DiscussionDetailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
