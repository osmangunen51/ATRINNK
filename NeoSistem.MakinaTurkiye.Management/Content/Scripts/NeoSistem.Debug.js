// NeoSistem.js
//


Type.registerNamespace('NeoSistem');

////////////////////////////////////////////////////////////////////////////////
// NeoSistem.AjaxOptions

NeoSistem.$create_AjaxOptions = function NeoSistem_AjaxOptions() { return {}; }


////////////////////////////////////////////////////////////////////////////////
// NeoSistem.Ajax

NeoSistem.Ajax = function NeoSistem_Ajax() {
}
NeoSistem.Ajax.Execute = function NeoSistem_Ajax$Execute(options) {
    /// <summary>
    /// Model Post
    /// </summary>
    /// <param name="options" type="NeoSistem.AjaxOptions">
    /// Parameters
    /// </param>
    var wRequest = new Sys.Net.WebRequest();
    try {
        wRequest.set_url(options.get_url());
        if (options.get_data() != null) {
            wRequest.set_body(NeoSistem.Ajax.ObjectSerialize(options.get_data()));
        }
        wRequest.set_body('&X-Requested-With=XMLHttpRequest');
        var verb = options.get_verb().toUpperCase();
        var isGetOrPost = (verb === 'GET' || verb === 'POST');
        if (!isGetOrPost) {
            wRequest.set_body(wRequest.get_body() + '&X-HTTP-Method-Override=' + verb);
        }
        wRequest.set_url(options.get_url());
        wRequest.set_httpVerb(verb);
        if (isGetOrPost) {
            wRequest.set_httpVerb(verb);
        }
        else {
            wRequest.set_httpVerb('POST');
            wRequest.get_headers()['X-HTTP-Method-Override'] = verb;
        }
        if (verb === 'PUT') {
            wRequest.get_headers()['Content-Type'] = 'application/x-www-form-urlencoded;';
        }
        wRequest.get_headers()['X-Requested-With'] = 'XMLHttpRequest';
        if (options.begin != null) {
            options.begin(wRequest.get_executor());
        }
        wRequest.add_completed(Function.createDelegate(null, function(executor) {
            var statusCode = executor.get_statusCode();
            if ((statusCode >= 200 && statusCode < 300) || statusCode === 304 || statusCode === 1223) {
                if (options.success != null) {
                    options.success(executor);
                }
            }
            else {
                if (options.failure != null) {
                    options.failure(executor);
                }
            }
        }));
        wRequest.invoke();
    }
    catch ($e1) {
        if (options.failure != null) {
            options.failure(wRequest.get_executor());
        }
    }
    finally {
        if (options.complete != null) {
            options.complete(wRequest.get_executor());
        }
    }
}
NeoSistem.Ajax.ObjectSerialize = function NeoSistem_Ajax$ObjectSerialize(model) {
    /// <summary>
    /// Model serizabe
    /// </summary>
    /// <param name="model" type="Object">
    /// Object Model
    /// </param>
    /// <returns type="String"></returns>
    var body = new Sys.StringBuilder();
    var $dict1 = model;
    for (var $key2 in $dict1) {
        var item = { key: $key2, value: $dict1[$key2] };
        body.append(String.format('{0}={1}&', item.key, item.value));
    }
    return body.toString();
}


////////////////////////////////////////////////////////////////////////////////
// NeoSistem.Util

NeoSistem.Util = function NeoSistem_Util() {
}
NeoSistem.Util.ViewRender = function NeoSistem_Util$ViewRender(view) {
    /// <param name="view" type="String">
    /// </param>
    window.document.write(view);
}
NeoSistem.Util._disableEnableControls = function NeoSistem_Util$_disableEnableControls(formname, disabled) {
    /// <param name="formname" type="String">
    /// </param>
    /// <param name="disabled" type="Boolean">
    /// </param>
    var form = document.getElementById(formname);
    for (var i = 0; i < form.elements.length; i++) {
        form.elements[i].disabled = disabled;
    }
}
NeoSistem.Util.enable = function NeoSistem_Util$enable(formname) {
    /// <param name="formname" type="String">
    /// </param>
    NeoSistem.Util._disableEnableControls(formname, false);
}
NeoSistem.Util.disable = function NeoSistem_Util$disable(formname) {
    /// <param name="formname" type="String">
    /// </param>
    NeoSistem.Util._disableEnableControls(formname, true);
}


NeoSistem.Ajax.registerClass('NeoSistem.Ajax');
NeoSistem.Util.registerClass('NeoSistem.Util');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.5.5.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
