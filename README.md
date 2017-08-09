# Moves-Dictionnary

The Moves Dictionnary is an hhmm-based unity project that aims to build a gesture game for pedagogical purposes. Each pedagogical step uses Microphone, gyroscope, magnetometer, accelerometer and tactile modes to emphasise sens throught sounds, vibrations, and help learners in their spatial mapping process. Magnetic or sound 3D space mapping is particulary appropriated for kindergarden children.  

This demonstrator was developed using **Smarthone** sensors and **IRCAM** algorithm libraries for real time gesture recognition. The library and all mathematical concepts are developped in Jules Françoise PhD thesis. 

* Presentation: https://www.julesfrancoise.com/phdthesis/
* Ircam Unity native plugin: https://github.com/Ircam-RnD/xmm-unity

# First Step :  Studying the data:

The project inculdes 2 scenes : First scene creates a gesture database for each recorded "Label". A label is then recorded in a .xml file using the following attributes <Mode><label><data>.  

* Labels recording :

![alt_tag](https://github.com/MehHam/Moves-Dictionnary/blob/master/Images/Labels-%20Record.png)

- Record a null movement (the Non-movement ) : click on the "null" button, perform your movement during the 5 seconds duration
- Record another movement : enter the label name in the input field, click on record, perform your movement during the 5 seconds duration.
  
![alt_tag](https://github.com/MehHam/Moves-Dictionnary/blob/master/Images/Sequences.png)

* Sequences recording :

![alt_tag](https://github.com/MehHam/Moves-Dictionnary/blob/master/Images/Letters%20Learning.png)

- Available gestures are abailable by clicking on the "Loader" button, slide the selected gesture into the sequence field and click on save sequence once it is complet. 

All the database is saved in Application.persistentDataPath+"/dataStore/" path. Each gesture is saved in a .xml file. All sequences are saved in sequences00.xml file. 

* Word teaching

In the second scene, for which a level design is definitly required, one has to enter a sequence ( a set of letters, a word, etc...) and gestures are controlled using each gesture mode. 

# Technical data exploration :
* Further explorations of the "Null" Movement, declared as "NoN ", is required. Especially the dynamical additive constant.
* further exploration of the magnetic data variation as respect to the permanent magnet intensity.
* A better study of the data behaviour as respect to the hhmm.SetStates(20); hmm.SetLikelihoodWindow(3); hhmm.SetRelativeRegularization(0.01f); hhmm.SetGaussians(1), for a better control of data.
* The hhmm plugin behaviour as respect to two consecutive identical letters is necessary since likelihoods seem unclear.

# Second Step :  The magic slate or VR
Using an ingame procedure that will be diffused on a DIY magic slate or VR carboard. 


