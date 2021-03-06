﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Phnx.AspNetCore.Modals
{
    /// <summary>
    /// Extensions for <see cref="IHtmlHelper"/> related to modals
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Render all session modals using a partial view
        /// </summary>
        /// <typeparam name="TModal">The type of modal to render</typeparam>
        /// <param name="helper">The <see cref="IHtmlHelper"/> to extend rendering</param>
        /// <param name="modalManager">The modal manager which contains all the modals to render</param>
        /// <param name="partialViewName">The name of the partial view to use when rendering</param>
        /// <param name="clearModalsAfterRendering">Whether to clear the modals from the session after they have all been rendered</param>
        /// <returns>All the modals rendered as HTML</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> or <paramref name="modalManager"/> or <paramref name="partialViewName"/> is <see langword="null"/></exception>
        public static IHtmlContent RenderModals<TModal>(this IHtmlHelper helper, IModalManager<TModal> modalManager, string partialViewName, bool clearModalsAfterRendering = true) where TModal : IModalViewModel
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }
            if (modalManager is null)
            {
                throw new ArgumentNullException(nameof(modalManager));
            }
            if (partialViewName is null)
            {
                throw new ArgumentNullException(nameof(partialViewName));
            }

            IHtmlContentBuilder content = new HtmlContentBuilder();

            foreach (var modal in modalManager.Get())
            {
                content.AppendHtml(helper.Partial(partialViewName, modal));
            }

            if (clearModalsAfterRendering)
            {
                modalManager.Clear();
            }

            return content;
        }
    }
}
