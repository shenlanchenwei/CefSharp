// Copyright © 2021 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;

namespace CefSharp.WinForms.Host
{
    /// <summary>
    /// Chromium Host Control Interface used by WinForms as a common reference
    /// for <see cref="ChromiumWebBrowser"/> and <see cref="ChromiumHostControl"/>
    /// </summary>
    public interface IChromiumHostControl
    {
        /// <summary>
        /// Event handler for receiving Javascript console messages being sent from web pages.
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread. 
        /// (The exception to this is when you're running with settings.MultiThreadedMessageLoop = false, then they'll be the same thread).
        /// </summary>
        event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;

        /// <summary>
        /// Event handler for changes to the status message.
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang.
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread. 
        /// (The exception to this is when you're running with settings.MultiThreadedMessageLoop = false, then they'll be the same thread).
        /// </summary>
        event EventHandler<StatusMessageEventArgs> StatusMessage;

        /// <summary>
        /// Event handler that will get called when the browser begins loading a frame. Multiple frames may be loading at the same
        /// time. Sub-frames may start or continue loading after the main frame load has ended. This method may not be called for a
        /// particular frame if the load request for that frame fails. For notification of overall browser load status use
        /// OnLoadingStateChange instead.
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread. 
        /// </summary>
        /// <remarks>Whilst this may seem like a logical place to execute js, it's called before the DOM has been loaded, implement
        /// <see cref="IRenderProcessMessageHandler.OnContextCreated"/> as it's called when the underlying V8Context is created
        /// </remarks>
        event EventHandler<FrameLoadStartEventArgs> FrameLoadStart;

        /// <summary>
        /// Event handler that will get called when the browser is done loading a frame. Multiple frames may be loading at the same
        /// time. Sub-frames may start or continue loading after the main frame load has ended. This method will always be called
        /// for all frames irrespective of whether the request completes successfully.
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread. 
        /// </summary>
        event EventHandler<FrameLoadEndEventArgs> FrameLoadEnd;

        /// <summary>
        /// Event handler that will get called when the resource load for a navigation fails or is canceled.
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread. 
        /// </summary>
        event EventHandler<LoadErrorEventArgs> LoadError;

        /// <summary>
        /// Event handler that will get called when the Loading state has changed.
        /// This event will be fired twice. Once when loading is initiated either programmatically or
        /// by user action, and once when loading is terminated due to completion, cancellation of failure. 
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread. 
        /// </summary>
        event EventHandler<LoadingStateChangedEventArgs> LoadingStateChanged;

        /// <summary>
        /// Occurs when the browser address changed.
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread.
        /// </summary>
        event EventHandler<AddressChangedEventArgs> AddressChanged;

        /// <summary>
        /// Occurs when the browser title changed.
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread.
        /// </summary>
        event EventHandler<TitleChangedEventArgs> TitleChanged;

        /// <summary>
        /// Event called after the underlying CEF browser instance has been created. 
        /// It's important to note this event is fired on a CEF UI thread, which by default is not the same as your application UI
        /// thread. It is unwise to block on this thread for any length of time as your browser will become unresponsive and/or hang..
        /// To access UI elements you'll need to Invoke/Dispatch onto the UI Thread.
        /// </summary>
        event EventHandler IsBrowserInitializedChanged;

        /// <summary>
        /// Loads the specified <paramref name="url"/> in the Main Frame.
        /// Same as calling <see cref="Load(string)"/>
        /// </summary>
        /// <param name="url">The URL to be loaded.</param>
        /// <remarks>
        /// This is exactly the same as calling Load(string), it was added
        /// as the method name is more meaningful and easier to discover
        /// via Intellisense.
        /// </remarks>
        void LoadUrl(string url);

        /// <summary>
        /// A flag that indicates whether the WebBrowser is initialized (true) or not (false).
        /// </summary>
        /// <value><c>true</c> if this instance is browser initialized; otherwise, <c>false</c>.</value>
        /// <remarks>In the WPF control there are two IsBrowserInitialized properties, the ChromiumWebBrowser.IsBrowserInitialized
        /// property is implemented as a Dependency Property and fully supports data binding. This property
        /// can only be called from the UI Thread. The explicit IWebBrowser.IsBrowserInitialized interface implementation that
        /// can be called from any Thread.</remarks>
        bool IsBrowserInitialized { get; }

        /// <summary>
        /// A flag that indicates whether the WebBrowser has been disposed (<see langword="true" />) or not (<see langword="false" />)
        /// </summary>
        /// <value><see langword="true" /> if this instance is disposed; otherwise, <see langword="false" /></value>
        bool IsDisposed { get; }

        /// <summary>
        /// A flag that indicates whether the control is currently loading one or more web pages (true) or not (false).
        /// </summary>
        /// <value><c>true</c> if this instance is loading; otherwise, <c>false</c>.</value>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool IsLoading { get; }

        /// <summary>
        /// A flag that indicates whether the state of the control current supports the GoBack action (true) or not (false).
        /// </summary>
        /// <value><c>true</c> if this instance can go back; otherwise, <c>false</c>.</value>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool CanGoBack { get; }

        /// <summary>
        /// A flag that indicates whether the state of the control currently supports the GoForward action (true) or not (false).
        /// </summary>
        /// <value><c>true</c> if this instance can go forward; otherwise, <c>false</c>.</value>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        bool CanGoForward { get; }

        /// <summary>
        /// The address (URL) which the browser control is currently displaying.
        /// Will automatically be updated as the user navigates to another page (e.g. by clicking on a link).
        /// </summary>
        /// <value>The address.</value>
        /// <remarks>In the WPF control, this property is implemented as a Dependency Property and fully supports data
        /// binding.</remarks>
        string Address { get; }

        /// <summary>
        /// Attempts to give focus to the IWebBrowser control.
        /// </summary>
        /// <returns><c>true</c> if keyboard focus and logical focus were set to this element; <c>false</c> if only logical focus
        /// was set to this element, or if the call to this method did not force the focus to change.</returns>
        bool Focus();

        /// <summary>
        /// Returns the current IBrowser Instance or null.
        /// <see cref="IBrowser"/> is the the underlying CefBrowser
        /// instance and provides access to frames/browserhost etc.
        /// </summary>
        IBrowser BrowserCore { get; }
    }
}
