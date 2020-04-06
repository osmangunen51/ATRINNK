
Type.registerNamespace('NeoSistem');NeoSistem.$create_AjaxOptions=function(){return {};}
NeoSistem.Ajax=function(){}
NeoSistem.Ajax.Execute=function(options){var $0=new Sys.Net.WebRequest();try{$0.set_url(options.get_url());if(options.get_data()!=null){$0.set_body(NeoSistem.Ajax.ObjectSerialize(options.get_data()));}$0.set_body('&X-Requested-With=XMLHttpRequest');var $1=options.get_verb().toUpperCase();var $2=($1==='GET'||$1==='POST');if(!$2){$0.set_body($0.get_body()+'&X-HTTP-Method-Override='+$1);}$0.set_url(options.get_url());$0.set_httpVerb($1);if($2){$0.set_httpVerb($1);}else{$0.set_httpVerb('POST');$0.get_headers()['X-HTTP-Method-Override']=$1;}if($1==='PUT'){$0.get_headers()['Content-Type']='application/x-www-form-urlencoded;';}$0.get_headers()['X-Requested-With']='XMLHttpRequest';if(options.begin!=null){options.begin($0.get_executor());}$0.add_completed(Function.createDelegate(null,function($p1_0){
var $1_0=$p1_0.get_statusCode();if(($1_0>=200&&$1_0<300)||$1_0===304||$1_0===1223){if(options.success!=null){options.success($p1_0);}}else{if(options.failure!=null){options.failure($p1_0);}}}));$0.invoke();}catch($3){if(options.failure!=null){options.failure($0.get_executor());}}finally{if(options.complete!=null){options.complete($0.get_executor());}}}
NeoSistem.Ajax.ObjectSerialize=function(model){var $0=new Sys.StringBuilder();var $dict1=model;for(var $key2 in $dict1){var $1={key:$key2,value:$dict1[$key2]};$0.append(String.format('{0}={1}&',$1.key,$1.value));}return $0.toString();}
NeoSistem.Util=function(){}
NeoSistem.Util.ViewRender=function(view){window.document.write(view);}
NeoSistem.Util.$0=function($p0,$p1){var $0=document.getElementById($p0);for(var $1=0;$1<$0.elements.length;$1++){$0.elements[$1].disabled=$p1;}}
NeoSistem.Util.enable=function(formname){NeoSistem.Util.$0(formname,false);}
NeoSistem.Util.disable=function(formname){NeoSistem.Util.$0(formname,true);}
NeoSistem.Ajax.registerClass('NeoSistem.Ajax');NeoSistem.Util.registerClass('NeoSistem.Util');
// ---- Do not remove this footer ----
// This script was generated using Script# v0.5.5.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
