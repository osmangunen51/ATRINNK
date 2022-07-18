/// <reference path="JQuery.js" />
/// <reference path="MicrosoftAjax.js" />  

////////////////////////////////////////////////////////////////////////////////
// FilterOptions

window.$create_FilterOptions = function FilterOptions() { return {}; }


////////////////////////////////////////////////////////////////////////////////
// FilterModel

window.FilterModel = function FilterModel() {
  /// <field name="_controllerName" type="String">
  /// </field>
  /// <field name="_model" type="Object">
  /// </field>
  /// <field name="_loadElementId" type="String">
  /// </field>
  /// <field name="_loadingElementId" type="String">
  /// </field>
  /// <field name="_options" type="FilterOptions">
  /// </field>
}
FilterModel.prototype = {
  _controllerName: null,

  get_controllerName: function FilterModel$get_controllerName() {
    /// <value type="String"></value>
    return this._controllerName;
  },
  set_controllerName: function FilterModel$set_controllerName(value) {
    /// <value type="String"></value>
    this._controllerName = value;
    return value;
  },

  _action: 'Index',

  get_action: function FilterModel$get_action() {
    /// <value type="String"></value>
    return this._action;
  },
  set_action: function FilterModel$set_action(value) {
    /// <value type="String"></value>
    this._action = value;
    return value;
  },
   
  _model: null,

  get_model: function FilterModel$get_model() {
    /// <value type="Object"></value>
    return this._model;
  },
  set_model: function FilterModel$set_model(value) {
    /// <value type="Object"></value>
    this._model = value;
    return value;
  },

  _loadElementId: null,

  get_loadElementId: function FilterModel$get_loadElementId() {
    /// <value type="String"></value>
    return this._loadElementId;
  },
  set_loadElementId: function FilterModel$set_loadElementId(value) {
    /// <value type="String"></value>
    this._loadElementId = value;
    return value;
  },

  _loadingElementId: null,

  get_loadingElementId: function FilterModel$get_loadingElementId() {
    /// <value type="String"></value>
    return this._loadingElementId;
  },
  set_loadingElementId: function FilterModel$set_loadingElementId(value) {
    /// <value type="String"></value>
    this._loadingElementId = value;
    return value;
  },

  refreshModel: function FilterModel$refreshModel(entry) {
    /// <param name="entry" type="Array" elementType="DictionaryEntry">
    /// </param>
    if (this.get_model() == null) {
      this.set_model({});
    }
    var $dict1 = entry;
    for (var $key2 in $dict1) {
      var item = { key: $key2, value: $dict1[$key2] };
      this.get_model()[item.key] = item.value;
    }
  },

  _options: null,

  get_options: function FilterModel$get_options() {
    /// <value type="FilterOptions"></value>
    return this._options;
  },
  set_options: function FilterModel$set_options(value) {
    /// <value type="FilterOptions"></value>
    this._options = value;
    return value;
  },

  register: function FilterModel$register(options) {
    /// <param name="options" type="FilterOptions">
    /// </param>
    this.set_options(options);
  }
}
 
FilterModel.registerClass('FilterModel');

var filterModel = new FilterModel();

function readModel() {
  filterModel.refreshModel({
    OrderName: $('#OrderName').val(),
    Order: $('#Order').val(),
    Page: $('#Page').val()
  });
  filterModel.get_options().read(filterModel, filterModel.get_model());
}

function RegisterHidden(columnName) {
  window.document.writeln('<input type="hidden" name="OrderName" id="OrderName" value="' + columnName + '" />');
  window.document.writeln('<input type="hidden" name="Order" id="Order" value="DESC" />');
  window.document.writeln('<input type="hidden" name="Page" id="Page" value="1" />');
}

function Delete(Id) {
   if (confirm('Kaydý silmek istediðinizden eminmisiniz ?')) {
    $.ajax({
      url: '/' + filterModel.get_controllerName() + '/Delete',
      data: { id: Id },
      type: 'post',
      dataType: 'json',
      success: function (result) {
        if (result) {
          $('#row' + Id).hide();
        }
      },
      error: function (x) {
        alert(x.responseText);
      }
    });
  }
}

function clearSearch(Id) {
  $('#' + Id).val('');
  Search();
}

function Order(orderName, e) {
  $('.HeaderDown').removeClass('HeaderDown');
  $(e).addClass('HeaderDown');
  $('#Order').val(($('#Order').val() == 'DESC' ? 'ASC' : 'DESC'));
  $('#OrderName').val(orderName);
  Search();
}

function Page(page) {
  $('#Page').val(page);
  Search();
}

var busy = false;


function Search() {
  readModel();
  $(filterModel.get_loadingElementId()).show();
  $.ajax({
    url: '/' + filterModel.get_controllerName() + '/' + filterModel.get_action(),
    type: 'post',
    data: filterModel.get_model(),
    success: function (data) {
      $(filterModel.get_loadElementId()).html(data);
      $(filterModel.get_loadingElementId()).hide();
      busy = false;
    },
    error: function (x, a, r) {
      $(filterModel.get_loadingElementId()).hide();
      busy = false;
    }
  });
}


function ThreadSearch() {
  if (!busy) {
    setTimeout(Search, 1000);
    busy = true;
  }
}

$(document).ready(function () {
  $('.date').datepicker();
  $('.Search').keyup(ThreadSearch).change(Search);
});