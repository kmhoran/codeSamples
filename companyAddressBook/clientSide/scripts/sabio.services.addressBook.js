//====================================================================
// sabio.services
//====================================================================
sabio.services.profile = sabio.services.profile || {};

sabio.services.public = sabio.services.public || {};

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// AJAX GET ALL PROFILES
//sabio.services.public.getAllEmployeesByCompanyId = function (id, onAjaxSuccess, onAjaxError) {
//    // setting the route prefix + userId
//    var url = "/api/company/" + id;
//    // establish the ajax load
//    var settings = {
//        cache: false,
//        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
//        dataType: "json",
//        success: onAjaxSuccess,
//        error: onAjaxError,
//        // Establish type of ajax call
//        type: "GET"

//    };

//    // call the ajax
//    $.ajax(url, settings);

//};


//Controllers/Api/CompanyEmployeeApiController.cs
sabio.services.public.getAllEmployeesByCompanyId = function (payload, onAjaxSuccess, onAjaxError) {
    // setting the route prefix + userId
    var url = "/api/company/" + payload.companyId;
    // establish the ajax load
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        data: payload,
        success: onAjaxSuccess,
        error: onAjaxError,
        // Establish type of ajax call
        type: "GET"

    };

    // call the ajax
    $.ajax(url, settings);

};

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// AJAX UPDATE PROFILE
sabio.services.public.updateProfile = function (id, data, onAjaxSuccess, onAjaxError) {
    var url = "/api/profile/" + id;

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        dataType: "json",
        success: onAjaxSuccess,
        error: onAjaxError,
        type: "PUT"
    };
    $.ajax(url, settings);
};
