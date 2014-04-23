using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraAudio : Mozart {

	public static string BONGO_SONG = "Bongo Song";
	public static string CONVERGENCE = "Convergence";
	public static string FOREST_SLUMBER = "Forest Slumber";
	public static string FOREST_ENVIRONMENT = "Forest_environment";
	public static string MAZU_NATION = "Mazu Nation";
	public static string PENTA_GONE = "Penta Gone";
	public static string PROPHECY = "Prophecy";
	public static string ROBOT_RESONANT = "Robot Resonant";
	public static string SERENITY = "Serenity";
	public static string FOREST_HAPPY = "Siphon_Forest_Happy";
	public static string THE_HEALER = "The_Healer";
	public static string TRANQUILITY = "Tranquility";
	public static string ISLANDER = "Islander";
	
	public string currentTrack;

	//Currently, there are only two song tracks...
	public override void Awake() {
		clips = new Dictionary<string, AudioClip>() {
			{BONGO_SONG, 		Resources.Load<AudioClip>(BONGO_SONG)},
			{CONVERGENCE, 		Resources.Load<AudioClip>(CONVERGENCE)},
			{FOREST_SLUMBER, 	Resources.Load<AudioClip>(FOREST_SLUMBER)},
			{FOREST_ENVIRONMENT,Resources.Load<AudioClip>(FOREST_ENVIRONMENT)},
			{MAZU_NATION, 		Resources.Load<AudioClip>(MAZU_NATION)},
			{PENTA_GONE, 		Resources.Load<AudioClip>(PENTA_GONE)},
			{PROPHECY, 		 	Resources.Load<AudioClip>(PROPHECY)},
			{ROBOT_RESONANT, 	Resources.Load<AudioClip>(ROBOT_RESONANT)},
			{SERENITY, 		 	Resources.Load<AudioClip>(SERENITY)},
			{FOREST_HAPPY, 		Resources.Load<AudioClip>(FOREST_HAPPY)},
			{THE_HEALER, 		Resources.Load<AudioClip>(THE_HEALER)},
			{ISLANDER, 		    Resources.Load<AudioClip>(ISLANDER)},
			{TRANQUILITY, 		Resources.Load<AudioClip>(TRANQUILITY)}
		};
	}

}