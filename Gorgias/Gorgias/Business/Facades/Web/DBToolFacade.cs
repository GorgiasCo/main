using Gorgias.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Gorgias.Business.Facades.Web
{
    public class DBToolFacade
    {

        public bool BulkProfileInsert(string list)
        {
            string[] profiles = getLines(list);

            foreach (string obj in profiles)
            {
                string[] parts = getLineParts(obj, "#");
                if (parts.Length >= 6)
                {
                    string[] profile = getLineParts(parts[0], ",,");
                    string[] tags = getLineParts(parts[2], ",,");
                    string[] address = getLineParts(parts[3], ",,");
                    string[] industries = getLineParts(parts[1], ",,");
                    string[] social = getLineParts(parts[4], "@");
                    string[] external = getLineParts(parts[5], "@");

                    Profile objProfile = setupProfile(profile);
                    objProfile.Addresses = new List<Address>();
                    objProfile.Addresses.Add(setupAddress(address));
                    int ProfileID = BusinessLayer.Facades.Facade.ProfileFacade().Insert(objProfile).ProfileID;
                    setupTags(tags, ProfileID);
                    setupIndustries(industries, ProfileID);
                    setupExternalLinks(external, ProfileID);
                    setupSocialNetworks(social, ProfileID);
                }
                else {
                    string[] profile = getLineParts(parts[0], ",,");
                    string[] tags = getLineParts(parts[2], ",,");
                    string[] address = getLineParts(parts[3], ",,");
                    string[] industries = getLineParts(parts[1], ",,");
                    string[] social = getLineParts(parts[4], "@");                    

                    Profile objProfile = setupProfile(profile);
                    objProfile.Addresses = new List<Address>();
                    objProfile.Addresses.Add(setupAddress(address));
                    int ProfileID = BusinessLayer.Facades.Facade.ProfileFacade().Insert(objProfile).ProfileID;
                    setupTags(tags, ProfileID);
                    setupIndustries(industries, ProfileID);                    
                    setupSocialNetworks(social, ProfileID);
                }                
            }
            return true;
        }

        private void setupSocialNetworks(string[] socialString, int ProfileID)
        {
            foreach (string obj in socialString)
            {
                string[] socialParts = getLineParts(obj, ",,");
                ProfileSocialNetworkDTO objSocial = new ProfileSocialNetworkDTO();
                objSocial.ProfileID = ProfileID;
                objSocial.SocialNetworkID = short.Parse(socialParts[0]);
                objSocial.ProfileSocialNetworkURL = socialParts[1];
                BusinessLayer.Facades.Facade.ProfileSocialNetworkFacade().Insert(objSocial);
            }
        }

        private void setupExternalLinks(string[] externalString, int ProfileID)
        {
            foreach (string obj in externalString)
            {
                string[] externalParts = getLineParts(obj, ",,");
                ExternalLinkDTO objExternal = new ExternalLinkDTO();
                objExternal.ProfileID = ProfileID;
                objExternal.LinkTypeID = short.Parse(externalParts[0]);
                objExternal.ExternalLinkURL= externalParts[1];
                BusinessLayer.Facades.Facade.ExternalLinkFacade().Insert(objExternal);
            }
        }

        private void setupIndustries(string[] industriesString, int ProfileID)
        {
            foreach (string obj in industriesString)
            {
                ProfileIndustryDTO objProfileIndustry = new ProfileIndustryDTO();
                objProfileIndustry.ProfileID = ProfileID;

                Industry objIndustry = new Industry();
                objIndustry.IndustryName = obj;
                objIndustry.IndustryImage = "NA";
                objIndustry.IndustryStatus = true;
                objIndustry.IndustryDescription = "#" + obj;
                objProfileIndustry.IndustryID = BusinessLayer.Facades.Facade.IndustryFacade().Insert(objIndustry).IndustryID;
                BusinessLayer.Facades.Facade.ProfileIndustryFacade().Insert(objProfileIndustry);
            }
        }

        private Address setupAddress(string[] addressString)
        {
            Address objAddress = new Address();
            objAddress.CityID = BusinessLayer.Facades.Facade.CityFacade().GetCity(addressString[1]).CityID;
            objAddress.AddressZipCode = addressString[2];
            objAddress.AddressTypeID = 1;
            objAddress.AddressAddress = addressString[0];
            objAddress.AddressName = "HQ";
            objAddress.AddressStatus = true;
            objAddress.AddressTel = addressString[4];
            //Need Location Geo ;)            
            return objAddress;
        }

        private void setupTags(string[] tagsString, int ProfileID)
        {
            List<ProfileTagDTO> listTags = new List<ProfileTagDTO>();
            foreach (string obj in tagsString)
            {
                ProfileTagDTO objTag = new ProfileTagDTO();
                objTag.TagName = obj;
                objTag.ProfileTagStatus = true;
                objTag.ProfileID = ProfileID;
                BusinessLayer.Facades.Facade.ProfileTagFacade().Insert(objTag);
            }
        }

        private Profile setupProfile(string[] profileString)
        {
            Profile obj = new Profile();
            obj.ProfileDateCreated = DateTime.Now;
            obj.ProfileDescription = "";
            obj.ProfileEmail = profileString[1];
            obj.ProfileFullname = profileString[0];
            obj.ProfileImage = profileString[0];
            obj.ProfileIsConfirmed = true;
            obj.ProfileIsDeleted = false;
            obj.ProfileIsPeople = true;
            obj.ProfileLike = 0;
            obj.ProfileShortDescription = "";
            obj.ProfileStatus = true;
            obj.ProfileTypeID = short.Parse(profileString[4]);
            obj.ProfileURL = profileString[2];
            obj.SubscriptionTypeID = short.Parse(profileString[3]);
            obj.ThemeID = 1;

            return obj;
        }

        private string[] getLines(string list)
        {
            string[] lines = Regex.Split(list, "\r\n");
            return lines;
        }

        private string[] getLineParts(string list, string split)
        {
            string[] parts = Regex.Split(list, split);
            return parts;
        }

    }
}