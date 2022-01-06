using System;
using System.Net.Http;

using Game.Helpers;

namespace UnitTests.TestHelpers
{
    /// <summary>
    /// Sample data for each of the Json packets from the server
    /// 
    /// Used in the UT to simulate the server responses
    /// 
    /// </summary>
    public static class JsonSampleData
    {
        #region APIExample
        public static StringContent StringContent_Example_API_Pass = new(
            @"{
                'status':1,
                'message':'Ok',
            }");

        public static StringContent StringContent_Example_API_Fail = new(
            @"{
                'status':0,
                'message':'error',
            }");
        #endregion APIExample

        #region ItemGet
        public static StringContent StringContentItemGetDefault = new(
                @"
                    {
                    'msg': 'Ok',
                    'errorCode': 0,
                    'version': '1.1.1.1',
                    'data': {
                    'ItemList': [
                    {
                    'Value': 9,
                    'Attribute': 14,
                    'Location': 20,
                    'Name': 'Strong Shield',
                    'Guid': '7ce2fbb2-2dd1-2f18-fda2-c7ad941eef8b',
                    'Description': 'Enough to hide behind',
                    'ImageURI': 'http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png',
                    'Range': 0,
                    'Damage': 0,
                    'Count': -1,
                    'IsConsumable': false,
                    'Category': 50
                    },
                    {
                    'Value': 9,
                    'Attribute': 14,
                    'Location': 20,
                    'Name': 'Bow',
                    'Guid': '86b958b3-e646-29b1-5dd8-270c7b21dcb8',
                    'Description': 'Fast shooting bow',
                    'ImageURI': 'http://cliparts.co/cliparts/di4/oAB/di4oABdbT.png',
                    'Range': 10,
                    'Damage': 6,
                    'Count': -1,
                    'IsConsumable': false,
                    'Category': 50
                    }
                    ]
                    }
                    }
                "
            );

        public static StringContent StringContentItemGet3 = new(
        @"
                    {
                    'msg': 'Ok',
                    'errorCode': 0,
                    'version': '1.1.1.1',
                    'data': {
                    'ItemList': [
                    {
                    'Value': 9,
                    'Attribute': 14,
                    'Location': 20,
                    'Name': 'Strong Shield',
                    'Guid': '7ce2fbb2-2dd1-2f18-fda2-c7ad941eef8b',
                    'Description': 'Enough to hide behind',
                    'ImageURI': 'http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png',
                    'Range': 0,
                    'Damage': 0,
                    'Count': -1,
                    'IsConsumable': false,
                    'Category': 50
                    },
                    {
                    'Value': 9,
                    'Attribute': 14,
                    'Location': 20,
                    'Name': 'Bow',
                    'Guid': '86b958b3-e646-29b1-5dd8-270c7b21dcb8',
                    'Description': 'Fast shooting bow',
                    'ImageURI': 'http://cliparts.co/cliparts/di4/oAB/di4oABdbT.png',
                    'Range': 10,
                    'Damage': 6,
                    'Count': -1,
                    'IsConsumable': false,
                    'Category': 50
                    },
                    {
                    'Value': 9,
                    'Attribute': 14,
                    'Location': 20,
                    'Name': 'Sling Shot',
                    'Guid': '86b958b3-e646-29b1-5dd8-270c7b21dcb9',
                    'Description': 'Number 3',
                    'ImageURI': 'http://cliparts.co/cliparts/di4/oAB/di4oABdbT.png',
                    'Range': 10,
                    'Damage': 6,
                    'Count': -1,
                    'IsConsumable': false,
                    'Category': 50
                    }
                    ]
                    }
                    }
                "
    );

        #endregion ItemGet

        #region ItemPost
        public static StringContent StringContentItemPostDefault = new(
                @"
                    {
                        'msg': 'Ok',
                        'errorCode': 0,
                        'version': '1.1.1.1',
                        'data': {
                            'ItemList': [
                                {
                                    'Value': 1,
                                    'Attribute': 10,
                                    'Location': 22,
                                    'Name': 'Rolex',
                                    'Guid': '4a02a998-c32a-369a-8301-61cede5d2f0b',
                                    'Description': 'A watch to tell the time',
                                    'ImageURI': 'http://www.clker.com/cliparts/9/4/b/f/1195423655761618219analog_wristwatch_stella_01.svg.med.png',
                                    'Range': 2,
                                    'Damage': 0,
                                    'Count': -1,
                                    'IsConsumable': false,
                                    'Category': 12
                                }
                            ]
                        }
                   }
                ");
        #endregion ItemPost


        //#region ClinicExamples
        //// Sample Clinic
        //public static StringContent StringContentExampleClinic = new(
        //    @"{
        //        'status':1,
        //        'message':'Ok',
        //        'version':'1.1.1.1',
        //        'id':'ID',
        //        'clinicID':'ClinicID',
        //        'prefix':'Prefix',
        //        'timeOut' :10000,
        //        'readingCaptureCount':1,
        //        'imageCompression':70,
        //        'transmitSuccessImage':false,
        //        'transmitFailImage':false,
        //        'transmitTelemetry':false,

        //        'offerPatientHistory' : true,
        //        'offerSurvey' : true,
        //        'offerCalculation' : true,
        //        'offerBiliPic' : true,
        //        'offerTcB' : true,
        //        'offerEye' : true,
        //        'offerNotes' : true,
        //        'offerLaboratory' : true,
        //        'offerPicture' : true,
        //        'offerG6PD' : true,
        //        'offerG6PDPhoto' : true,
        //        'offerStatus' : true,
        //        'offerBirthInformation' : true,
        //        'offerDebug' : true,
        //        'allowAutoGeoLocation' : true,

        //        'hospitalList':[
        //                {
        //                'name':'name1',
        //                'address': 'address1',
        //                'latitude':'lat',
        //                'longitude':'long',
        //                'phone':'phone1',
        //                'hospitalType':1,
        //                'sequence':1
        //                },
        //                {
        //                'name':'name2',
        //                'address': 'address2',
        //                'latitude':'lat2',
        //                'longitude':'long2',
        //                'phone':'phone2',
        //                'hospitalType':1,
        //                'sequence':3
        //                }
        //            ]
        //    }");

        //#endregion ClinicExamples

    }
}