using System.IO;
using System.Media;
using System;

namespace chatbot_part02
{
    public class audioPlay
    {
        public audioPlay()
        {
            string project_location = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(project_location);
            //replacing the bin\debug\ so we can get the audio
            string updated_path = project_location.Replace("bin\\Debug\\", "");
            // combining the wav name as sound.wav with the updated path
            string full_path = Path.Combine(updated_path, "myAudio.wav");

            // passing it to the method play_wav
            Play_wav(full_path);

        }// end of the constructor
        //method to play the sound
        private void Play_wav(string full_path)
        {

            //try and catch
            try
            {
                //or play the sound 
                using (SoundPlayer player = new SoundPlayer(full_path))
                {
                    //to play the sound till and use this
                    player.PlaySync();
                }//end of using
            }
            catch (Exception error)
            {
                //showing the error message
                Console.WriteLine(error.Message);
            }//end of try and catch

        
    

        }
    }
}