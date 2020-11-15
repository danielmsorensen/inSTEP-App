using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inSTEP : MonoBehaviour {

    public AWS aws;
    public UI ui;

    bool inGame;
    string classID;
    new string name;
    string dataType;

    bool requesting;

    public void CheckClass() {
        classID = ui.loginClassID.text;
        if (!string.IsNullOrEmpty(classID)) {
            ui.LoginHideError();

            ui.loadingPage.SetActive(true);
            aws.CheckClassID(classID, valid => {
                ui.loadingPage.SetActive(false);

                if (valid) {
                    ui.nameTitle.SetText(classID);
                    ui.PageName();
                }
                else {
                    ui.LoginShowError("Invalid Class ID");
                }
            }, exception => {
                ui.loadingPage.SetActive(false);
                Debug.LogError(exception);
            });
        }
        else {
            ui.LoginShowError("Invalid Class ID");
        }
    }

    public void JoinClass() {
        name = ui.nameName.text;

        if (!string.IsNullOrEmpty(name)) {
            ui.NameHideError();

            ui.loadingPage.SetActive(true);
            aws.JoinClass(classID, name, valid => {
                ui.loadingPage.SetActive(false);

                if (valid) {
                    ui.gameTitle.SetText(classID);
                    ui.PageGame();

                    inGame = true;
                }
                else {
                    ui.NameShowError("Name Already Taken");
                }
            }, exception => {
                ui.loadingPage.SetActive(false);
                Debug.LogError(exception);
            });
        }
        else {
            ui.NameShowError("Name Empty");
        }
    }

    public void LeaveClass() {
        inGame = false;

        ui.PageLogin();
        /*
        ui.loadingPage.SetActive(true);
        aws.LeaveClass(() => {
            ui.loadingPage.SetActive(false);
        }, exception => {
            ui.loadingPage.SetActive(false);
            Debug.LogError(exception);
        });*/
    }

    void Update() {
        if (inGame && !requesting) {
            if (Time.time % 1 <= Time.deltaTime) {
                object data = null;
                switch (dataType) {
                    default:
                    case "":
                        ui.gameText.SetText("In Game");
                        break;
                    case "ClassIDInvalid":
                        inGame = false;
                        ui.PageLogin();
                        break;
                    case "stepCounter":
                        if (StepCounter.current != null) {
                            data = StepCounter.current.stepCounter;
                            ui.gameText.SetText(data.ToString());
                        }
                        break;
                }

                requesting = true;
                aws.SendData(classID, name, data, dataType => {
                    requesting = false;
                    this.dataType = dataType;
                }, exception => {
                    requesting = false;
                    Debug.LogError(exception);
                });
            }
        }
    }
}
