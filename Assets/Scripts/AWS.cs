using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Lambda;
using Amazon.Lambda.Model;

public class AWS : MonoBehaviour {

    CognitoAWSCredentials credentials;
    AmazonLambdaClient lambda;

    void Awake() {
        UnityInitializer.AttachToGameObject(gameObject);

        credentials = new CognitoAWSCredentials("eu-west-2:793871cb-c8a9-45bf-b20d-97df75ddbce7", RegionEndpoint.EUWest2);
        lambda = new AmazonLambdaClient(credentials, RegionEndpoint.EUWest2);
    }

    public void CheckClassID(string classID, Action<bool> success, Action<Exception> error) {
        lambda.InvokeAsync(new InvokeRequest() {
            FunctionName = "classIDValid",
            Payload = $@"{{
                ""classID"": ""{classID}""
            }}"
        }, result => {
            if (result.Exception == null) {
                success.Invoke(JsonUtility.FromJson<Payload>(System.Text.Encoding.ASCII.GetString(result.Response.Payload.ToArray())).body == "ClassIDValid");
            }
            else {
                error.Invoke(result.Exception);
            }
        });
    }

    public void JoinClass(string classID, string name, Action<bool> success, Action<Exception> error) {
        lambda.InvokeAsync(new InvokeRequest() {
            FunctionName = "joinClass",
            Payload = $@"{{
                ""classID"": ""{classID}"",
                ""studentName"": ""{name}""
            }}"
        }, result => {
            if (result.Exception == null) {
                success.Invoke(JsonUtility.FromJson<Payload>(System.Text.Encoding.ASCII.GetString(result.Response.Payload.ToArray())).body == "StudentNameSet");
            }
            else {
                error.Invoke(result.Exception);
            }
        });
    }

    public void LeaveClass(Action success, Action<Exception> error) {
        lambda.InvokeAsync(new InvokeRequest() {
            FunctionName = "",
            Payload = "{}"
        }, result => {
            if (result.Exception == null) {
                success.Invoke();
            }
            else {
                error.Invoke(result.Exception);
            }
        });
    }

    public void SendData(string classID, string name, object data, Action<string> success, Action<Exception> error) {
        lambda.InvokeAsync(new InvokeRequest() {
            FunctionName = "dataCollection",
            Payload = $@"{{
                ""classID"": ""{classID}"",
                ""studentName"": ""{name}"",
                ""data"": ""{data}""
            }}"
        }, result => {
            if (result.Exception == null) {
                success.Invoke(JsonUtility.FromJson<Payload>(System.Text.Encoding.ASCII.GetString(result.Response.Payload.ToArray())).body);
            }
            else {
                error(result.Exception);
            }
        });
    }

    public struct Payload {
        public int statusCode;
        public string body;
    }
}