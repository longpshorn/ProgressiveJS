ProgressiveJS
=============

A library to simplify the creation of progressively enhanced ajax web applications.

More to come...





<h3>AJAX Progressive Enhancement for Forms / Elements</h3>


<h4>Routing-based DOM-ready Execution</h4>
<p>ProgressiveJS uses a conventions-based approach to JavaScript DOM-ready execution and initialization.</p>

<h5>Background &amp; Motivation</h5>
<p>
    This pattern was inspired by a Paul Irish blog post by the name, <a href="http://www.paulirish.com/2009/markup-based-unobtrusive-comprehensive-dom-ready-execution/">Markup-based Unobtrusive DOM-ready Execution</a>,
    and a subsequent Jason Garber blog post expanding on Irish's initial implementation <a href="http://viget.com/inspire/extending-paul-irishs-comprehensive-dom-ready-execution">found here</a>.
</p>
<p>The pattern was slightly modified for ProgressiveJS to work with the ASP.NET MVC framework.</p>
<p>
    The pattern allows a developer to rely on a specific set of JavaScript to be initialized on DOM-ready for a specific page.
    More precisely, for the <kbd>HomeController</kbd>'s <kbd>Index</kbd> method, the developer can rely on the <kbd>app.home.index()</kbd> JavaScript method being called on page load.
</p>
<p>
    Additionally, when the <kbd>~/Home/Index</kbd> route is hit, JavaScript calls are made to the <kbd>app.common.init()</kbd>, <kbd>app.home.init()</kbd>, <kbd>app.home.index()</kbd>, <kbd>app.home.finalize()</kbd>, and <kbd>app.common.finalize()</kbd> methods, in that order.
    These calls allow for page-specific, as well as site-wide, JavaScript initialization to take place.
</p>


<h5>Implementation</h5>

<p>The implementation of the Markup-based Unobtrusive Comprehensive DOM-ready Execution functionality relies on a few stipulations within the code.</p>
<p>
    First, the <kbd>body</kbd> tag is required to be identified by the controller and action from which the MVC route was resolved. Note that the controller name is also added as a <kbd>class</kbd>
    for the <kbd>body</kbd> element. With these conventions in place, this pattern can be used for JavaScript initialization and CSS namespacing on a page-specific, or controller-specific basis.
</p>

<pre class="prettyprint"><code class="language-html">@@{
    var controller = ViewContext.RouteData.Values["controller"].ToString().ToLower();
    var action = ViewContext.RouteData.Values["action"].ToString().ToLower();
}
&lt;body id="@@controller-@@action" class="@@controller"&gt;
</code></pre>

<p>Next, the <kbd>util.js</kbd> file will be the main entry point for the application. It contains the DOM-ready initialization code.</p>

<pre class="prettyprint"><code class="language-js">// HTML5 Dom-Ready Execution Utility
var util = {
    init: function (context) {
        var classNames = $(document).find('body')[0].getAttribute('id').split('-'),
            controller = classNames[0],
            action = classNames[1];

        // if they exist, call the methods on the context in the following order
        // common.init(context);
        // controller.init(context);
        // controller.action(context);
        // controller.finalize(context);
        // common.finalize(context);
        util.exec('common', 'init', context);
        if (controller) {
            util.exec(controller, 'init', context);
        }
        if (action) {
            util.exec(controller, action, context);
        }
        if (controller) {
            util.exec(controller, 'finalize', context);
        }
        util.exec('common', 'finalize', context);
    },
    exec: function (controller, action, context) {
        // if the action is not passed, use init as the default,
        // otherwise, use the action passed.
        action = (action === undefined) ? 'init' : action;

        // if the context isn't passed, the context should be the entire document,
        // otherwise, restrict the context to the passed context.
        context = (context !== undefined && context !== null)
            ? $(context)
            : $(document);

        // if we have a value for controller and the controller is contained within
        // the namespace object and the namespace has a method that is a function,
        // call that method.
        if (controller !== '' && app[controller] && typeof app[controller][action] === 'function') {
            app[controller][action](context);
        }
    }
};

// document ready initialization
$(function () {
    util.init();
});
</code></pre>

<p>
    A final deviation from the original Garber-Irish implementation is that the ProgressiveJS <kbd>util.init()</kbd> method can be passed a "<kbd>context</kbd>" varible.
    This <kbd>context</kbd> is meant to provide the developer with a global JavaScript initialization entry-point that can be re-called if needed to re-initialized JavaScript functionality on the page in the event new content is added dynamically.
    If no <kbd>context</kbd> is passed to the <kbd>util.init()</kbd> method, the <kbd>$(document)</kbd> itself will serve as the <kbd>context</kbd>.
    The <kbd>$(document)</kbd> serves as the <kbd>context</kbd> for the initial call to <kbd>util.init()</kbd>.
</p>


<h5>Example</h5>
<p>As a simple example of this pattern's use, we will look at the <kbd>~/Home/Index</kbd> method to see it in action.</p>

<pre class="prettyprint"><code class="language-js">// From app.common.js... shortened for clarity

if (app === undefined) {
    app = {};
}

app.common = {
    init: function (context) {
        app.log('common init start');
        var ajax = app.ajax,
            ux = app.ux;

        $.validator.unobtrusive.parse(context);
        ux.navigation.init(context);
        ux.init_datepickers(context);
        ajax.init_ajax_forms(context);
        ajax.init_ajax_updaters(context);
        app.log('common init end');
    },

    finalize: function (context) {
        app.log('common finalize start');
        var ux = app.ux;
        ux.init_text_overflow(context);
        ux.joyride.init(context);
        app.log('common finalize end');
    }
};


// From app.home.js...

app.home = {
    index: function (context) {
        app.home.search.init(context);
        app.home.mapping.init(context);
    }
};
</code></pre>

<p>
    In this example, since we are hitting the <kbd>~/Home/Index</kbd> method, we know that the body tag will have an id of <kbd>id="home-index"</kbd>.
    We also know from the call to <kbd>util.init();</kbd> that this id will be parsed and calls will be made to <kbd>app.common.init()</kbd>, <kbd>app.home.init()</kbd>, <kbd>app.home.index()</kbd>, <kbd>app.home.finalize()</kbd>, and <kbd>app.common.finalize()</kbd>.
    So, we can rely on various site-wide initializations to take place such as navigation and datepicker initialization from common.init.
    Then, for the <kbd>~/Home/Index</kbd> page, we can rely on app.home.search.init() and app.home.mapping.init() being called to initialize JavaScript-enabled search and mapping functionality.
</p>
<p>Also of note is that the context variable is being passed to the initialization methods which will use the context to determine whether or not the functionality to be initialized will apply for the given page or not.</p>








<h5>Background &amp; Motivation</h5>

<p>
    With the <strong><a href="#location1">Routing-based DOM-ready Execution (RDOME)</a></strong> conventions in place, a standardized approach to progressively enhancing native web behavior that
    allows for dynamic addition/removal/modification of content and reinitialization of JavaScript functionality after DOM content is updated. By leveraging the conventions laid out with
    RDOME, much of the pain associated with re-inventing the wheel when developing an AJAX application can be alleviated.
</p>

<p>When developing a progressively-enhanced AJAX application, a general pattern is repeated on the client over and over again. This approach involves the following steps:</p>

<ol>
    <li>
        Rendering HTML and waiting for a user to submit a form or click a link
<pre class="prettyprint"><code class="language-html">&lt;form id="progressively-enhanced-form" method="post" action="/some/request/location"&gt;
    &lt;label for="login"&gt;Login:&lt;/label&gt;
    &lt;input type="text" id="login" name="login" /&gt;
    &lt;label for="password"&gt;Password:&lt;/label&gt;
    &lt;input type="password" id="password" name="password" /&gt;
    &lt;button type="submit"&gt;Login&lt;/button&gt;
&lt;/form&gt;
</code></pre>
    </li>
    <li>
        Intercepting the form submit event or the link's click event
<pre class="prettyprint"><code class="language-js">$('form#progressively-enhanced-form').on('submit', function(e) {
    e.preventDefault(); // cancels the native submit behavior
    ...
});
</code></pre>
    </li>
    <li>
        Forwarding the request on to the server via an AJAX call
<pre class="prettyprint"><code class="language-js">$('form#progressively-enhanced-form').on('submit', function(e) {
    e.preventDefault(); // cancels the native submit behavior
    var $form = $(this);
    $.ajax({
        type: $form.attr('method'), // defines the type of HTTP request
        url: $form.attr('action'), // defines the server endpoint to which the request will be sent
        data: { // defines the data that will be sent with the request
            login: $form.find('[name="login"]').val(),
            password: $form.find('[name="password"]').val()
        }
    });
});
</code></pre>
    </li>
    <li>
        Handling to the server's response (usually by replacing/adding DOM content and initializing JavaScript functionality on this content)
<pre class="prettyprint"><code class="language-js">$('form#progressively-enhanced-form').on('submit', function(e) {
    ...
    $.ajax({
        ...
    })
    .done(function(response) {
        // put the server's response into the page in the location defined by the "#update-target"
        $('#update-target').html(response);
        // initialize any required JavaScript functionality within the newly added "#update-target"
        $('#update-target').initializeJavaScriptFunctionality();
    });
});
</code></pre>
    </li>
</ol>

<p>
    While this approach is simple and straightforward in theory, in practice, repeatedly coding this functionality and maintaining numerous versions of similar functionality can become
    cumbersome and lead to hours of wasted time and inefficiency. Libraries exist, such as the ASP.NET AJAX libary, that abstract some of the plumbing required to set up this functionality,
    but they don't extend well for more complex use cases. Also, in general, these libraries don't rely on any conventions related to the approach to handling the server's response.
    This leaves much of the responsibility of determining how to distribute the new content and initialize the JavaScript to the developer.
</p>

<p>
    Having to constantly repeat oneself to apply this approach, not being able to handle more complex situations without writing custom code, and having to manually configure which JavaScript
    functionality should be enabled after every asynchronous request all drive the need for a generalized solution to this problem.
</p>

<h5>Implementation</h5>

<p>The pattern used within ProgressiveJS relies on a small set of custom JavaScript and C# libraries that alleviates the pain of re-inventing the wheel when developing this sort of AJAX application.</p>
<p>
    In order to ease the application of the AJAX pattern described above, a small amount of JavaScript code is used to intercept submit and click events, forward the requests via an AJAX call,
    and handle the server's response. As there are 2 primary methods in which an ajax request is made, there are 2 helper functions that assist in the creation of these approaches. For this document,
    we will refer to these 2 helper functions as the AjaxForm and AjaxUpdater helpers.
</p>

<p>
    To fully understand how this approach is applied, you will first need to understand the conventions that are applied surrounding the type of responses
    that will be recieved from the server. We can represent the object type with JavaScript Object Notation (JSON) as shown below.
</p>

<pre class="prettyprint"><code class="language-js">var ExampleJsonAjaxResponse = {
    // The mainResponse will provide a response that will be used to update the primary target as specified by the
    // AjaxForm and AjaxUpdater helpers. The specifications for the AjaxForm and AjaxUpdater helpers will be defined later
    mainResponse: {
        // A string that can be used to provide the user with a status message related to their request, or,
        // in simpler use cases, can be used to contain the entire response when not status message is required.
        message: 'So long and thanks for all the fish!',
        // Can be any of the various jQuery selector methods, i.e., html, replaceWith, append, etc., or any custom jQuery plugin
        manipulation: 'html',
        // A generic status code that represents the <em>status</em> of the response that can help when displaying the mainResponse.message to the user
        status: 'Default' // Can be one of 'Default', 'Success', 'Warning', 'Error'
    },

    // An array of additional JsonAjaxResponseItems that allows the developer to specify (from the server-side)
    // additional data elements that should be updated or commands that should be called when this response is recieved.
    items: [
        {
            // The jQuery style selector that can be used to <em>select</em> the targetted element to update
            selector: '#user-name',
            // Can be any of the various jQuery selector methods, i.e., html, replaceWith, append, etc., or any custom jQuery plugin
            manipulation: 'html',
            // An array / list of items that will be passed as parameters to the jQuery method specified by the <em>manipulation</em> property
            args: ['Patrick Smith'],
            // A boolean indication of whether or not JavaScript initialization of the content within this item should be completed
            reinitializeJs: true
        },
        ...
    ],

    // A generic status code string that represents the <em>status</em> of the response
    // Can be user-defined value with the exeption of the reserve code of "DoRedirect"
    // which is used by the helper libraries to perform a redirect after an AJAX request is completed.
    statusCode: 'Default'
};
</code></pre>

<p>
    This JsonAjaxResponse represents the expected response from the server when the AjaxForm and AjaxUpdater helpers are used. When responses are recieved from the server,
    since the AjaxForm and AjaxUpdater helpers can expect responses in the above format, a prescriptive and patterned approach can be used to handle the response. Specifically,
    the response should be parsed, updates should be made to specified DOM elements using the appropriate <em>manipulation</em> method, and JavaScript functionality should be
    conditionally reapplied based on the value of the <em>reinitializeJs</em> property.
</p>

<h6>Client-side Helper Libraries</h6>
<p>
    The client-side helpers for the AjaxForm and AjaxUpdater is located in the <kbd>app.ajax.js</kbd> file. We will discuss the details of their client-side implementation in this section.
</p>

<p>The AjaxForm and AjaxUpdater client-side helpers use a set of common defaults as provided below.</p>

<pre class="prettyprint"><code class="language-js">defaults: {
    // A boolean indication of whether or not the target specified for the AjaxForm or AjaxUpdater is a <em>globalTarget</em>
    // or if it exists within the form being submitted.
    globalTarget: true,
    // The target that should be updated by the mainResponse from the server's JsonAjaxResponse
    target: '.response',
    // A boolean indication of whether a form should be <em>cleared</em> following the ajax request completion
    clearForm: false,
    // A boolean indication of whether a form should be <em>reset</em> following the ajax request completion
    resetForm: true,
    // A comma-separated list of selectors for additional fields that are not within the form that should be sent with the request
    // e.g., '#StateDataParameter1,#StateDataParameter2'
    additionalFields: null,
    // In the event that a successful AJAX request should result in the user being redirected to a new page, the locationOnSuccess
    // parameter can be used to specify that location.
    locationOnSuccess: null
}
</code></pre>

<p>
    With these defaults, AjaxForm and AjaxUpdater functionality can be parameterized to allow for generalized use in several scenarios.
    A brief outline of the code to handle a server's JsonAjaxResponse is provided below.
</p>

<pre class="prettyprint"><code class="language-js">reloadAndReinitContent: function (response, opts) {
    // Extend the defaults with the options specfied for the specific instantiation
    // of the AjaxForm or AjaxUpdater
    opts = $.extend(true, {}, app.ajax.defaults, opts);

    // Find the primary update target depending on whether it is a <em>global</em> target or within the form
    var $t = opts.globalTarget ? $(opts.target) : $(opts.target, opts.form);

    if (!$.isPlainObject(response)) {
        // If the response is <em>NOT</em> a <em>plain object</em> in the JavaScript sense,
        // it will have already been added to the DOM by the <kbd>jquery.ajaxForm.js</kbd> library.
        // All we need to do here is reinitialize the JavaScript functionality for the update target.
        util.init($t);
    } else {
        // By convention, we expect the response to be a <em>JsonAjaxReponse</em>.
        // Process the response as such.

        // First, process the main response
        var mainResponse = response.mainResponse;
        if (mainResponse !== undefined && mainResponse != null) {
            if ($t.is('.top-note__content') && mainResponse.message != null) {
                // Uses the <em>topNote</em> ux library to display the status message to the user
                // in the event the target is the ".top-note__content" element.
                app.ux.topNote.activate(mainResponse.message, mainResponse.status.toLowerCase());
            } else {
                $t[mainResponse.manipulation](mainResponse.message);
                // we may have lost the reference to the target so we need to reselect it
                $t = opts.globalTarget ? $(opts.target) : $(opts.target, opts.form);
                util.init($t);
            }
        }

        // Then, process the items
        var arr = response.items;
        for (var i in arr) {
            var el = $(arr[i].selector),
                manip = arr[i].manipulation,
                args = arr[i].args;
            if (el.length && manip !== '') {
                var content = args;
                if (manip === 'replaceWith') {
                    content = $(arr[i].args[0]);
                }
                el[manip].apply(el, content);

                // we may have lost the reference to the target so we need to reselect it
                if (manip !== 'replaceWith') content = $(arr[i].selector);

                if (manip === 'after' || manip === 'insertAfter') content = content.next();
                else if (manip === 'before' || manip === 'insertBefore') content = content.prev();
                else if (manip === 'wrap') content = content.parent();

                if (arr[i].reinitializeJs) util.init(content);
            }
        }
    }
    return opts;
}
</code></pre>

<p>
    In summary, the code above handles the server's JsonAjaxResponse by making calls to the commands that the server requested.
    The initialization of a AjaxForm or AjaxUpdater method is completed with a call to methods within the <kbd>app.ajax.js</kbd> file.
    Namely, the <kbd>init_ajax_forms</kbd> and <kbd>init_ajax_updaters</kbd> methods. The <kbd>init_ajax_forms</kbd> method is provided below.
</p>

<pre class="prettyprint"><code class="language-js">init_ajax_forms: function (context) {
    var $forms = $('[data-ajaxform]', context);
    if ($forms.length) {
        $forms.each(function () {
            var $form = $(this),
                options = { form: $form },
                data = $form.data('ajaxform');
            // Make sure the data property is not empty
            if (data !== null && data !== undefined && data !== '') {
                app.ajax.ajaxSubmit(options);
            }
        });
    }
}
</code></pre>

<p>
    In short, the <em>context</em> is searched for forms with the data-ajax form attribute. For each form found with the data-ajaxform
    attribute, a call to <kbd>app.ajax.ajaxSubmit</kbd> will be made which completes the initialization of the AjaxForm. A similar
    approach is taken for AjaxUpdaters.
</p>

<h6>Server-side Helper Libraries</h6>

<p>
    In order to further ease the initialization of an AjaxForm or AjaxUpdater, two C# classes by the same names have been created
    that allow a developer to serialize the options for a given form or updater element into a JSON string that can be read using
    the $elem.data() jQuery method. An example usage of the AjaxForm class is provided below.
</p>

<pre class="prettyprint"><code class="language-cs">@@using (Html.BeginForm("Login", "User", FormMethod.Post, new { data_ajaxform = new AjaxForm(".top-note__content", true, false).ToJson() }))
{
    @@Html.AntiForgeryToken()
    // Other fields...
}
</code></pre>

<p>
    The goal of the C# AjaxForm and AjaxUpdater helper classes is to simplify configuring an AjaxForm by removing the need
    to write boilerplate code to configure a given form or updater element. Specifically, the <kbd>.ToJson()</kbd> method
    provides the serialization functionality for the developer and, by wrapping the configuration inside a server-side model,
    most of the details of the configuration are abstracted away from the developer but are easily discoverable when they are
    needed.
</p>

<p>A rendered form would contain the serialized JSON and would look like</p>
@using (Html.BeginForm("Login", "User", FormMethod.Post, new { data_ajaxform = new AjaxForm(".top-note__content", true, false).ToJson() }))
{
    @Html.AntiForgeryToken()
}
<pre class="prettyprint"><code class="language-html">&lt;form method="post" action="/User/Login" data-ajaxform='{"Target":".top-note__content","GlobalTarget":true,"ResetForm":false,"ClearForm":false,"LocationOnSuccess":null,"AdditionalFields":null,"Callbacks":[]}'&gt;
    &lt;input name="__RequestVerificationToken" type="hidden" value="qgNFT_iBgwgeFNu30N_WA1c6v-..." /&gt;
    // Other fields...
&lt;form&gt;
</code></pre>

<p>
    The final piece of the AjaxForm and AjaxUpdater library is the creation of JsonAjaxResponses on the server. For this task,
    again, a set of helper classes has been created to ease the creation, packaging, and manipulation of the JsonAjaxResponse that
    will be sent back to the client for processing. The data being transported from the server can easily be created and placed into
    a JsonAjaxResponse by using a helper class by the name <kbd>JsonAjaxResult</kbd>. The <kbd>JsonAjaxResult</kbd> provides a
    <strong>chainable</strong> interface to make creation and addition to the result easy for the developer. An simple example of how this
    chainable interface is used is provided below.
</p>

<pre class="prettyprint"><code class="language-cs">public JsonAjaxResult UpdateSearchResults(SearchQueryParams searchQuery)
{
    // Create a new JsonAjaxResult that wraps and serializes a JsonAjaxResponse object
    // The JsonAjaxResult constructur is overloaded and can be passed
    //   - the <em>mainResponse</em> message,
    //   - the <em>manipulation</em> method,
    //   - the <em>status</em> of the mainResponse
    // In this example, we are just instantiating the JsonAjaxResult and not passing anything
    // as a mainResponseMessage since for this use case, we don't want to display a success message
    // to the user. If we decided later in the code that we did want to add a mainResponse message,
    // we could add it to the response with a call to the chainable <em>SetMessage</em> method.
    var result = new JsonAjaxResult()
        // The chainable Html method adds a JsonAjaxResponseItem to the <em>items</em> array.
        // The <em>Html</em> method in particular adds an item to the items array with a manipulation method
        // of "html".
        .Html(
            // The "#search-results" parameter here specifies the <em>selector</em> property of the JsonAjaxResponseItem
            "#search-results",
            // The RenderPartialViewToString is a Controller extension method to do what it says (render a partial view to a string)
            this.RenderPartialViewToString(
                // The partial view to render (can be fully qualified as
                // "~[Areas/{AreaName}]/Views/{ControllerName}/{ViewName.cshtml}" if necessary)
                "_AssetSearchResults",
                // In this case, GetHomeViewModel is a controller method that modularizes the creation of the HomeViewModel
                // given the set of search query params.
                GetHomeViewModel(searchQuery)
            )
        );

    // Return the JsonAjaxResult which inherits from ActionResult
    return result;
}
</code></pre>
