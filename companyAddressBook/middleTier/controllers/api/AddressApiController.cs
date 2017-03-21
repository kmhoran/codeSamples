using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/address")]

    public class AddressApiController : ApiController
    {
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [Dependency]
        public IAddressService _AddressService { get; set; }

        [Route(), HttpGet]
        [Authorize]
        public HttpResponseMessage AddressGetAll()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<AddressDomain> addressList = _AddressService.GetAllAddresses();

            var response = new ItemsResponse<AddressDomain> { Items = addressList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("quoterequest"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetByCompanyIdAndType([FromUri]AddressRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<AddressDomain> addressList = _AddressService.GetByCompanyIdAndAddressType(model);

            var response = new ItemsResponse<AddressDomain> { Items = addressList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("companylist/{CompanyId:int}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetAddressListByCompanyId([FromUri]AddressRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<AddressDomain> addressList = _AddressService.GetAddressesByCompanyId(model.CompanyId);

            var response = new ItemsResponse<AddressDomain> { Items = addressList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("{AddressId:int}"), HttpGet] 
        [Authorize]   
        public HttpResponseMessage AddressByIdGet([FromUri]AddressRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            AddressDomain address = _AddressService.GetAddressById(model);

            var response = new ItemResponse<AddressDomain> { Item = address };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route(), HttpPost]
        [Authorize]
        public HttpResponseMessage AddressInsert(AddressRequiredRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            int AddressId = _AddressService.InsertAddress(model);

            var response = new ItemResponse<int> { Item = AddressId };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("{id:int}"), HttpPut] 
        [Authorize]
        public HttpResponseMessage AddressEdit(AddressUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }     

            bool isSuccessful = _AddressService.UpdateAddress(model);

            var response = new ItemResponse<bool> { Item = isSuccessful };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("{AddressId:int}"), HttpDelete] 
        [Authorize]
        public HttpResponseMessage AddressDelete([FromUri]AddressDeleteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            bool isSuccessful = _AddressService.DeleteAddress(model.AddressId);

            var response = new ItemResponse<bool> { Item = isSuccessful};

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("addressbook/{CompanyId:int}"), HttpGet]
        public HttpResponseMessage GetAddressBookByCompanyId([FromUri]AddressRequest model)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var response = new ItemResponse<AddressBookDomain>
            {
                Item = _AddressService.GetAddressBookByCompanyId(model.CompanyId)
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("radius"), HttpPost]
        public HttpResponseMessage GetAllAddressesInRadius(LatLngRadiusRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var response = new ItemsResponse<AddressDistanceDomain>
            {
                Items = _AddressService.GetAddressesInRadius(model)
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
