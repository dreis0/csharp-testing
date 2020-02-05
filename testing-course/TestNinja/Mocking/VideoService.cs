using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        IFileReader _reader;
        IVideoRepository _repository;

        public VideoService(IFileReader reader = null, IVideoRepository repository = null)
        {
            _reader = reader ?? new FileReader();
            _repository = repository ?? new VideoRepository();
        }

        public string ReadVideoTitle()
        {
            var str = _reader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);

            if (video == null)
                return "Error parsing the video.";

            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            var videos = _repository.GetUnprocessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }


}