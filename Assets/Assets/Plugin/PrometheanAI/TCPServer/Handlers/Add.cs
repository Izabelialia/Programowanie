﻿using System;
using System.Collections.Generic;
using PrometheanAI.Modules.Utils;
using UnityEditor;
using UnityEngine;

namespace PrometheanAI.Modules.TCPServer.Handlers
{
    /// <summary>
    /// Adds new object to the Scene based on incoming data
    /// </summary>
    public class Add : ICommand
    {
        public string GetToken => "add";

        public void Handle(string rawCommandData, List<string> commandParametersString,
            Action<CommandHandleProcessState, string> callback) {
            if (commandParametersString.Count < 1) {
                callback.Invoke(CommandHandleProcessState.Failed, string.Empty);
                return;
            }

            var path = commandParametersString[0];
            path = path.Replace("\\", "");
            var name = "";
            var position = Vector3.zero;
            var rotation = Vector3.zero;
            var scale = Vector3.one;
            if (commandParametersString.Count > 1) {
                name = commandParametersString[1];
            }

            if (commandParametersString.Count > 2) {
                var positionsData = commandParametersString[2].Split(',');
                var positionVector = CommandUtility.StringArrayToVector(positionsData);
                position = CommandUtility.ConvertToMetersVector(positionVector);
            }

            if (commandParametersString.Count > 3) {
                var rotationData = commandParametersString[3].Split(',');
                rotation = CommandUtility.StringArrayToVector(rotationData);
            }

            if (commandParametersString.Count > 4) {
                var scaleData = commandParametersString[4].Split(',');
                scale = CommandUtility.StringArrayToVector(scaleData);
            }

            if (path != string.Empty) {
                var newObject =
                    (GameObject) PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(path));
                if (newObject != null) {
                    UndoUtility.RecordUndo(GetToken, newObject, true);
                    newObject.name = name;
                    newObject.transform.position = position;
                    newObject.transform.rotation = Quaternion.Euler(rotation);
                    newObject.transform.localScale = scale;
                }
            }

            callback.Invoke(CommandHandleProcessState.Success, string.Empty);
        }
    }
}