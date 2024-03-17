﻿using Entities.DTO; using Repository; using Service; using System.Net;  namespace ServiceImpl {     public class ProcessarVideoService : IProcessarVideoService     {          private readonly IServiceBusRepository _repository;          public ProcessarVideoService(IServiceBusRepository repository)         {             _repository = repository;         }          public async Task<HttpStatusCode> ProcessarVideo(List<Tuple<string, FileStream>> videos)         {             foreach (var video in videos)             {                  var enviarVideoRequest = new EnviarVideoRequest(video.Item1);                  await _repository.SendVideoInfoToQueueAsync(enviarVideoRequest);                                    //Send File to Blob Storage             }              return HttpStatusCode.OK;         }     } } 