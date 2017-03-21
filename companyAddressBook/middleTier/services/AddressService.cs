using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using System.Net;
using System.Xml.Linq;
using Sabio.Web.Enums;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class AddressService : BaseService, IAddressService
    {
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<AddressDomain> GetAllAddresses()
        {
            List<AddressDomain> addressList = null;
            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Address_GetAll"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              { }
              , map: delegate (IDataReader reader, short set)
              {
                  var singleAddress = new AddressDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleAddress.AddressId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.CompanyId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.Date = reader.GetSafeDateTime(startingIndex++);
                  singleAddress.Address1 = reader.GetSafeString(startingIndex++);
                  singleAddress.City = reader.GetSafeString(startingIndex++);
                  singleAddress.State = reader.GetSafeString(startingIndex++);
                  singleAddress.ZipCode = reader.GetSafeString(startingIndex++);
                  singleAddress.Latitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Longitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Slug = reader.GetSafeString(startingIndex++);
                  singleAddress.AddressType = reader.GetSafeInt32(startingIndex++);

                  if (addressList == null)
                  {
                      addressList = new List<AddressDomain>();
                  }

                  addressList.Add(singleAddress);
              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return addressList;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public AddressDomain GetAddressById(AddressRequest model)
        {
            AddressDomain address = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Address_GetById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@Id", model.AddressId);
              }, map: delegate (IDataReader reader, short set)
              {
                  address = new AddressDomain();

                  int startingIndex = 0; //startingOrdinal

                  address.AddressId = reader.GetSafeInt32(startingIndex++);
                  address.CompanyId = reader.GetSafeInt32(startingIndex++);
                  address.Date = reader.GetSafeDateTime(startingIndex++);
                  address.Address1 = reader.GetSafeString(startingIndex++);
                  address.City = reader.GetSafeString(startingIndex++);
                  address.State = reader.GetSafeString(startingIndex++);
                  address.ZipCode = reader.GetSafeString(startingIndex++);
                  address.Latitude = reader.GetSafeDecimal(startingIndex++);
                  address.Longitude = reader.GetSafeDecimal(startingIndex++);
                  address.Slug = reader.GetSafeString(startingIndex++);
                  address.AddressType = reader.GetSafeInt32(startingIndex++);
              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return address;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<AddressDomain> GetAddressesByCompanyAndType(int companyId, AddressType type)
        {
            List<AddressDomain> addressList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Address_Select_ByCompanyAndType"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@companyId", companyId);
                  paramCollection.AddWithValue("@addressType", type);
              }
              , map: delegate (IDataReader reader, short set)
              {
                  var singleAddress = new AddressDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleAddress.AddressId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.CompanyId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.Date = reader.GetSafeDateTime(startingIndex++);
                  singleAddress.Address1 = reader.GetSafeString(startingIndex++);
                  singleAddress.City = reader.GetSafeString(startingIndex++);
                  singleAddress.State = reader.GetSafeString(startingIndex++);
                  singleAddress.ZipCode = reader.GetSafeString(startingIndex++);
                  singleAddress.Latitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Longitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Slug = reader.GetSafeString(startingIndex++);
                  singleAddress.AddressType = reader.GetSafeInt32(startingIndex++);

                  if (addressList == null)
                  {
                      addressList = new List<AddressDomain>();
                  }

                  addressList.Add(singleAddress);
              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return addressList;
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<AddressDomain> GetByCompanyIdAndAddressType(AddressRequest model)
        {
            List<AddressDomain> addressList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Address_Select_ByCompanyAndType"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@companyId", model.CompanyId);
                  paramCollection.AddWithValue("@addressType", model.AddressType);
              }
              , map: delegate (IDataReader reader, short set)
              {
                  var singleAddress = new AddressDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleAddress.AddressId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.CompanyId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.Date = reader.GetSafeDateTime(startingIndex++);
                  singleAddress.Address1 = reader.GetSafeString(startingIndex++);
                  singleAddress.City = reader.GetSafeString(startingIndex++);
                  singleAddress.State = reader.GetSafeString(startingIndex++);
                  singleAddress.ZipCode = reader.GetSafeString(startingIndex++);
                  singleAddress.Latitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Longitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Slug = reader.GetSafeString(startingIndex++);
                  singleAddress.AddressType = reader.GetSafeInt32(startingIndex++);

                  if (addressList == null)
                  {
                      addressList = new List<AddressDomain>();
                  }

                  addressList.Add(singleAddress);
              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return addressList;
        }




        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public int InsertAddress(AddressRequiredRequest model)
        {
            int Id = 0;

            string addressString = String.Concat(model.Address1, ", ", model.City, ",  "
                , model.State, ", ", model.ZipCode);

            LatLngDomain latlng = AddressGetLatLng(addressString);

            model.Latitude = latlng.Latitude;
            model.Longitude = latlng.Longitude;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Address_Insert"
                      , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                      {

                          paramCollection.AddWithValue("@CompanyId", model.CompanyId);
                          paramCollection.AddWithValue("@Date", DateTime.Now);
                          paramCollection.AddWithValue("@Address1", model.Address1);
                          paramCollection.AddWithValue("@City", model.City);
                          paramCollection.AddWithValue("@State", model.State);
                          paramCollection.AddWithValue("@ZipCode", model.ZipCode);
                          paramCollection.AddWithValue("@Latitude", model.Latitude);
                          paramCollection.AddWithValue("@Longitude", model.Longitude);
                          paramCollection.AddWithValue("@Slug", model.Slug);
                          paramCollection.AddWithValue("@AddressType", model.AddressType);

                          var p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                          p.Direction = System.Data.ParameterDirection.Output;

                          paramCollection.Add(p);

                      }, returnParameters: delegate (SqlParameterCollection param)
                      {
                          int.TryParse(param["@Id"].Value.ToString(), out Id);
                      });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public bool UpdateAddress(AddressUpdateRequest model)
        {
            bool success = false;

            string addressString = String.Concat(model.Address1, ", ", model.City, ",  "
                , model.State, ", ", model.ZipCode);

            LatLngDomain latlng = AddressGetLatLng(addressString);

            model.Latitude = latlng.Latitude;
            model.Longitude = latlng.Longitude;


            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Address_Update"
                      , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                      {
                          paramCollection.AddWithValue("@CompanyId", model.CompanyId);
                          paramCollection.AddWithValue("@Date", model.Date);
                          paramCollection.AddWithValue("@Address1", model.Address1);
                          paramCollection.AddWithValue("@City", model.City);
                          paramCollection.AddWithValue("@State", model.State);
                          paramCollection.AddWithValue("@ZipCode", model.ZipCode);
                          paramCollection.AddWithValue("@Latitude", model.Latitude);
                          paramCollection.AddWithValue("@Longitude", model.Longitude);
                          paramCollection.AddWithValue("@Slug", model.Slug);
                          paramCollection.AddWithValue("@AddressType", model.AddressType);
                          paramCollection.AddWithValue("@Id", model.AddressId);

                          success = true;
                      });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return success;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public bool DeleteAddress(int id)
        {
            bool success = false;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Address_Delete"
                      , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                      {
                          paramCollection.AddWithValue("@Id", id);

                          success = true;
                      });

                return success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public AddressBookDomain GetAddressBookByCompanyId (int id)
        {
            AddressBookDomain addressBook = null;

            try
            {
                addressBook = new AddressBookDomain
                {
                    Primary = GetAddressesByCompanyAndType(id, AddressType.Primary),

                    Office = GetAddressesByCompanyAndType(id, AddressType.Office),

                    Warehouse = GetAddressesByCompanyAndType(id, AddressType.Warehouse),

                    ProjectSite = GetAddressesByCompanyAndType(id, AddressType.ProjectSite)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return addressBook;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public LatLngDomain AddressGetLatLng(string address)
        {
            // string address = model.Address1 + ", " + model.City + ",  " + model.State + ", " + model.ZipCode;

            LatLngDomain result = null;

            string requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement resultElem = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = resultElem.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");

            if (lat != null && lng != null)
            {
                Decimal latitude = Decimal.Parse(lat.Value);

                Decimal longitude = Decimal.Parse(lng.Value);
                result = new LatLngDomain
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
            }
            return result;

        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<AddressDistanceDomain> GetAddressesInRadius(LatLngRadiusRequest model)
        {
            List<AddressDistanceDomain> addressList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "Address_FindInRadius"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@latpoint", model.Latitude);
                  paramCollection.AddWithValue("@lngpoint", model.Longitude);
                  paramCollection.AddWithValue("@radius", model.Radius);
              }
              , map: delegate (IDataReader reader, short set)
              {
                  var singleAddress = new AddressDistanceDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleAddress.AddressId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.CompanyId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.Date = reader.GetSafeDateTime(startingIndex++);
                  singleAddress.Address1 = reader.GetSafeString(startingIndex++);
                  singleAddress.City = reader.GetSafeString(startingIndex++);
                  singleAddress.State = reader.GetSafeString(startingIndex++);
                  singleAddress.ZipCode = reader.GetSafeString(startingIndex++);
                  singleAddress.Latitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Longitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Slug = reader.GetSafeString(startingIndex++);
                  singleAddress.AddressType = reader.GetSafeInt32(startingIndex++);
                  singleAddress.Distance = (Decimal)reader.GetSafeDouble(startingIndex++);

                  if (addressList == null)
                  {
                      addressList = new List<AddressDistanceDomain>();
                  }

                  addressList.Add(singleAddress);
              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return addressList;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<AddressDomain> GetAddressesByCompanyId(int companyId)
        {
            List<AddressDomain> addressList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Address_GetByCompanyId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@companyId", companyId);
              }
              , map: delegate (IDataReader reader, short set)
              {
                  var singleAddress = new AddressDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleAddress.AddressId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.CompanyId = reader.GetSafeInt32(startingIndex++);
                  singleAddress.Date = reader.GetSafeDateTime(startingIndex++);
                  singleAddress.Address1 = reader.GetSafeString(startingIndex++);
                  singleAddress.City = reader.GetSafeString(startingIndex++);
                  singleAddress.State = reader.GetSafeString(startingIndex++);
                  singleAddress.ZipCode = reader.GetSafeString(startingIndex++);
                  singleAddress.Latitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Longitude = reader.GetSafeDecimal(startingIndex++);
                  singleAddress.Slug = reader.GetSafeString(startingIndex++);
                  singleAddress.AddressType = reader.GetSafeInt32(startingIndex++);

                  if (addressList == null)
                  {
                      addressList = new List<AddressDomain>();
                  }

                  addressList.Add(singleAddress);
              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return addressList;
        }



    }
}