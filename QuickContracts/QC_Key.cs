﻿/* 
QuickContracts
Copyright 2017 Malah

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using KSP.Localization;
using UnityEngine;

namespace QuickContracts {
	public partial class QGUI {
	
		Key SetKey = Key.None;
		Rect rectSetKey = new Rect();

		enum Key {
			None,
			DeclineSelectedContract,
			DeclineAllContracts,
			DeclineAllTest,
			AcceptSelectedContract
		}					
	
		KeyCode DefaultKey(Key key) {
			switch (key) {
			case Key.DeclineSelectedContract:
				return KeyCode.X;
			case Key.DeclineAllContracts:
				return KeyCode.C;
			case Key.DeclineAllTest:
				return KeyCode.V;
			case Key.AcceptSelectedContract:
				return KeyCode.A;
			}
			return KeyCode.None;
		}

		string GetText(Key key) {
			switch (key) {
			case Key.DeclineSelectedContract:
					return Localizer.Format("quickcontracts_declineSelected");
			case Key.DeclineAllContracts:
					return Localizer.Format("quickcontracts_declineAll");
			case Key.DeclineAllTest:
					return Localizer.Format("quickcontracts_declineTest");
			case Key.AcceptSelectedContract:
					return Localizer.Format("quickcontracts_acceptSelected");
			}
			return string.Empty;
		}

		KeyCode CurrentKey(Key key) {
			switch (key) {
			case Key.DeclineSelectedContract:
				return QSettings.Instance.KeyDeclineSelectedContract;
			case Key.DeclineAllContracts:
				return QSettings.Instance.KeyDeclineAllContracts;
			case Key.DeclineAllTest:
				return QSettings.Instance.KeyDeclineAllTest;
			case Key.AcceptSelectedContract:
				return QSettings.Instance.KeyAcceptSelectedContract;
			}
			return KeyCode.None;
		}

		void VerifyKey(Key key) {
			try {
				Input.GetKey(CurrentKey(key));
			} catch {
				Warning ("Wrong key: " + CurrentKey(key), "QGUI");
				SetCurrentKey (key, DefaultKey(key));
			}
		}

		void VerifyKey() {
			VerifyKey (Key.DeclineSelectedContract);
			VerifyKey (Key.DeclineAllContracts);
			VerifyKey (Key.DeclineAllTest);
			VerifyKey (Key.AcceptSelectedContract);
		}

		void SetCurrentKey(Key key, KeyCode currentKey) {
			switch (key) {
			case Key.DeclineSelectedContract:
				QSettings.Instance.KeyDeclineSelectedContract = currentKey;
				break;
			case Key.DeclineAllContracts:
				QSettings.Instance.KeyDeclineAllContracts = currentKey;
				break;
			case Key.DeclineAllTest:
				QSettings.Instance.KeyDeclineAllTest = currentKey;
				break;
			case Key.AcceptSelectedContract:
				QSettings.Instance.KeyAcceptSelectedContract = currentKey;
				break;
			}
		}

		void DrawSetKey(int id) {
			GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
			GUILayout.Label (Localizer.Format("quickcontracts_pressKey", GetText (SetKey)));
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (Localizer.Format("quickcontracts_clearAssign"), GUILayout.ExpandWidth (true), GUILayout.Height (30))) {
				SetCurrentKey (SetKey, KeyCode.None);
				SetKey = Key.None;
			}
			if (GUILayout.Button (Localizer.Format("quickcontracts_defaultAssign"), GUILayout.ExpandWidth (true), GUILayout.Height (30))) {
				SetCurrentKey (SetKey, DefaultKey (SetKey));
				SetKey = Key.None;
			}
			if (GUILayout.Button (Localizer.Format("quickcontracts_cancelAssign"), GUILayout.ExpandWidth (true), GUILayout.Height (30))) {
				SetKey = Key.None;
			}
			GUILayout.EndHorizontal ();
			GUILayout.EndVertical ();
		}

		void DrawConfigKey(Key key) {
			GUILayout.BeginHorizontal ();
			GUILayout.Label (string.Format ("{0}: <color=#FFFFFF><b>{1}</b></color>", GetText (key), CurrentKey (key)), GUILayout.Width (250));
			GUILayout.FlexibleSpace();
			if (GUILayout.Button (Localizer.Format("quickcontracts_set"), GUILayout.ExpandWidth (true), GUILayout.Height (20))) {
				SetKey = key;
			}
			GUILayout.EndHorizontal ();
		}
	}
}