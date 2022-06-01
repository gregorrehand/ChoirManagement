using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BLL.App.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PublicApi.DTO.v1;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;
using AppUser = Domain.App.Identity.AppUser;
using VoiceGroup = PublicApi.DTO.v1.VoiceGroup;

namespace TestProject.IntegrationTests
{
    public class ProjectIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;
        
        
        public ProjectIntegrationTests(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        // dont follow redirects
                        AllowAutoRedirect = false
                    }
                );
        }

        [Fact]
        public async void RegisterTest()
        {
            // ARRANGE
            var uri = "/api/v1/Account/register";
            
            var getTestResponse = await _client.GetAsync("/api/v1/VoiceGroups");
            
            getTestResponse.EnsureSuccessStatusCode();

            var voiceGroupBody = await (getTestResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(voiceGroupBody);

            var voiceGroupData = JsonHelper.DeserializeWithWebDefaults<List<VoiceGroup>>(voiceGroupBody);
            
            
            var registerDto = new PublicApi.DTO.v1.Register()
            {
                Email = "user@test.com",
                Firstname = "Eesnimi",
                Lastname = "Perekonnanimi",
                Password = "Foo.bar.12345678",
                VoiceGroupId = voiceGroupData!.First().Id
            };

            var jsonString = JsonConvert.SerializeObject(registerDto);

            // ACT
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);
            
            
            Assert.NotNull(data);
            Assert.NotNull(data!.Token);
            Assert.Equal("Eesnimi", data!.Firstname);
            Assert.Equal("Perekonnanimi", data.Lastname);
        }
        
        [Fact]
        public async void CreateProjectTest()
        {
            // ARRANGE
            var uri = "/api/v1/projects";

            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);

            var entity = new PublicApi.DTO.v1.ProjectAddDTO()
            {
                Name = "name",
                Info = "info",
                Programme = "asdasd"
            };
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<ProjectDTO>(body);
            
            
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("name", data!.Name);
            Assert.Equal("info", data!.Info);
            Assert.Equal("asdasd", data!.Programme);
        }

        [Fact]
        public async void CreateConcertTest()
        {
            // ARRANGE
            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            
            var getSeededResponse = await _client.GetAsync("/api/v1/Projects");
            
            getSeededResponse.EnsureSuccessStatusCode();

            var seededBody = await (getSeededResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededBody);

            var seededEntity = JsonHelper.DeserializeWithWebDefaults<List<Project>>(seededBody)!.First();
            Assert.NotNull(seededEntity);
            Assert.Equal("seededProjectName", seededEntity!.Name);
            
            var uri = "/api/v1/concerts";

            var nowTime = DateTime.Now;
            var entity = new PublicApi.DTO.v1.ConcertAddDTO()
            {
                Name = "name",
                Info = "info",
                Programme = "asdasd",
                Location = "loc",
                Start = nowTime,
                ProjectId = seededEntity.Id
            };

            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<ConcertDTO>(body);
            
            
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("name", data!.Name);
            Assert.Equal("info", data!.Info);
            Assert.Equal("asdasd", data!.Programme);
            Assert.Equal("loc", data!.Location);
            Assert.Equal(nowTime, data!.Start);
            Assert.Equal(seededEntity.Id, data!.ProjectId);
        }
        
        [Fact]
        public async void CreateRehearsalTest()
        {
            // ARRANGE
            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            
            var getSeededResponse = await _client.GetAsync("/api/v1/Projects");
            
            getSeededResponse.EnsureSuccessStatusCode();

            var seededBody = await (getSeededResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededBody);

            var seededEntity = JsonHelper.DeserializeWithWebDefaults<List<Project>>(seededBody)!.First();
            Assert.NotNull(seededEntity);
            Assert.Equal("seededProjectName", seededEntity!.Name);
            
            var uri = "/api/v1/rehearsals";

            var nowTime = DateTime.Now;
            var thenTime = DateTime.Now.AddHours(1);
            var entity = new PublicApi.DTO.v1.RehearsalAddDTO()
            {
                Info = "info",
                RehearsalProgramme = "asdasd",
                Location = "loc",
                Start = nowTime,
                End = thenTime,
                ProjectId = seededEntity.Id
            };

            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<RehearsalDTO>(body);
            
            
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("info", data!.Info);
            Assert.Equal("asdasd", data!.RehearsalProgramme);
            Assert.Equal("loc", data!.Location);
            Assert.Equal(nowTime, data!.Start);
            Assert.Equal(thenTime, data!.End);
            Assert.Equal(seededEntity.Id, data!.ProjectId);
        }
        
        [Fact]
        public async void CreatePersonConcertTest()
        {
            // ARRANGE
            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            
            var getSeededResponse = await _client.GetAsync("/api/v1/Projects");
            
            getSeededResponse.EnsureSuccessStatusCode();

            var seededBody = await (getSeededResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededBody);

            var seededEntity = JsonHelper.DeserializeWithWebDefaults<List<Project>>(seededBody)!.First();
            Assert.NotNull(seededEntity);
            Assert.Equal("seededProjectName", seededEntity!.Name);
            
            var uri = "/api/v1/concerts";

            var nowTime = DateTime.Now;
            var entity = new PublicApi.DTO.v1.ConcertAddDTO()
            {
                Name = "name",
                Info = "info",
                Programme = "asdasd",
                Location = "loc",
                Start = nowTime,
                ProjectId = seededEntity.Id
            };

            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var concert = JsonHelper.DeserializeWithWebDefaults<ConcertDTO>(body);
            
            
            Assert.NotNull(concert);
            Assert.NotEqual(Guid.Empty, concert!.Id);
            Assert.Equal("name", concert!.Name);
            Assert.Equal("info", concert!.Info);
            Assert.Equal("asdasd", concert!.Programme);
            Assert.Equal("loc", concert!.Location);
            Assert.Equal(nowTime, concert!.Start);
            Assert.Equal(seededEntity.Id, concert!.ProjectId);
            
            
            uri = "/api/v1/personconcerts";
            
            var usersResponse = await _client.GetAsync("/api/v1/Account/GetUsers");
            
            usersResponse.EnsureSuccessStatusCode();

            var seededUsers = await (usersResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededUsers);


            var appUsers = JsonHelper.DeserializeWithWebDefaults<List<AppUser>>(seededUsers);
            Assert.NotNull(appUsers);
            Assert.NotEqual(Guid.Empty, appUsers![0].Id);

            var personConcert = new PersonConcertAddDTO()
            {
                AppUserId = appUsers[1].Id,
                ConcertId = concert.Id
            };
            
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personConcert), Encoding.Default, "application/json"));

            getTestResponse.EnsureSuccessStatusCode();

            
            body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<PersonConcertDTO>(body);
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("pending", data!.Status);
            Assert.Equal(concert.Id, data!.ConcertId);
            Assert.Equal(appUsers[1].Id, data!.AppUserId);
            
            uri = "/api/v1/personconcerts/bulk";

            var personConcerts = new List<PersonConcertAddDTO>();
            personConcerts.Add(new PersonConcertAddDTO()
            {
                AppUserId = appUsers[2].Id,
                ConcertId = concert.Id
            });
            
            personConcerts.Add(new PersonConcertAddDTO()
            {
                AppUserId = appUsers[0].Id,
                ConcertId = concert.Id
            });
            
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personConcerts), Encoding.Default, "application/json"));
            getTestResponse.EnsureSuccessStatusCode();
            
            
            var getCreatedEntities = await _client.GetAsync("/api/v1/projects");
            
            getCreatedEntities.EnsureSuccessStatusCode();

            var responseBody = await (getCreatedEntities).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(responseBody);

            var entities = JsonHelper.DeserializeWithWebDefaults<List<ProjectDTO>>(responseBody);
            Assert.NotNull(entities);
            Assert.Equal(3, entities!.First().Concerts!.First().PersonConcerts!.Count());
            
            Assert.Equal("pending", entities!.First().Concerts!.First().PersonConcerts!.First().Status);
            Assert.Equal(appUsers[1].Id, entities!.First().Concerts!.First().PersonConcerts!.First().AppUser!.Id);
        }

        
        [Fact]
        public async void CreatePersonRehearsalTest()
        {
            // ARRANGE
            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            
            var getSeededResponse = await _client.GetAsync("/api/v1/Projects");
            
            getSeededResponse.EnsureSuccessStatusCode();

            var seededBody = await (getSeededResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededBody);

            var seededEntity = JsonHelper.DeserializeWithWebDefaults<List<Project>>(seededBody)!.First();
            Assert.NotNull(seededEntity);
            Assert.Equal("seededProjectName", seededEntity!.Name);
            
            var uri = "/api/v1/rehearsals";

            var nowTime = DateTime.Now;
            var thenTime = DateTime.Now.AddHours(1);
            var entity = new PublicApi.DTO.v1.RehearsalAddDTO()
            {
                Info = "info",
                RehearsalProgramme = "asdasd",
                Location = "loc",
                Start = nowTime,
                End = thenTime,
                ProjectId = seededEntity.Id
            };

            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var rehearsal = JsonHelper.DeserializeWithWebDefaults<RehearsalDTO>(body);
            
            
            Assert.NotNull(rehearsal);
            Assert.NotEqual(Guid.Empty, rehearsal!.Id);
            Assert.Equal("info", rehearsal!.Info);
            Assert.Equal("asdasd", rehearsal!.RehearsalProgramme);
            Assert.Equal("loc", rehearsal!.Location);
            Assert.Equal(nowTime, rehearsal!.Start);
            Assert.Equal(thenTime, rehearsal!.End);
            Assert.Equal(seededEntity.Id, rehearsal!.ProjectId);
            
            uri = "/api/v1/personrehearsals";
            
            var usersResponse = await _client.GetAsync("/api/v1/Account/GetUsers");
            
            usersResponse.EnsureSuccessStatusCode();

            var seededUsers = await (usersResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededUsers);


            var appUsers = JsonHelper.DeserializeWithWebDefaults<List<AppUser>>(seededUsers);
            Assert.NotNull(appUsers);
            Assert.NotEqual(Guid.Empty, appUsers![0].Id);

            var personRehearsal = new PersonRehearsalAddDTO()
            {
                AppUserId = appUsers[1].Id,
                RehearsalID = rehearsal.Id
            };
            
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personRehearsal), Encoding.Default, "application/json"));

            getTestResponse.EnsureSuccessStatusCode();

            
            body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<PersonRehearsalDTO>(body);
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("pending", data!.Status);
            Assert.Equal(rehearsal.Id, data!.RehearsalID);
            Assert.Equal(appUsers[1].Id, data!.AppUserId);
            
            //Test bulk
            uri = "/api/v1/personrehearsals/bulk";

            var personRehearsals = new List<PersonRehearsalAddDTO>();
            personRehearsals.Add(new PersonRehearsalAddDTO()
            {
                AppUserId = appUsers[2].Id,
                RehearsalID = rehearsal.Id
            });
            
            personRehearsals.Add(new PersonRehearsalAddDTO()
            {
                AppUserId = appUsers[0].Id,
                RehearsalID = rehearsal.Id
            });
            
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personRehearsals), Encoding.Default, "application/json"));
            getTestResponse.EnsureSuccessStatusCode();
            
            
            var getCreatedEntities = await _client.GetAsync("/api/v1/projects");
            
            getCreatedEntities.EnsureSuccessStatusCode();

            var responseBody = await (getCreatedEntities).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(responseBody);

            var entities = JsonHelper.DeserializeWithWebDefaults<List<ProjectDTO>>(responseBody);
            Assert.NotNull(entities);
            Assert.Equal(3, entities!.First().Rehearsals!.First().PersonRehearsals!.Count());
            
            Assert.Equal("pending", entities!.First().Rehearsals!.First().PersonRehearsals!.First().Status);
            Assert.Equal(appUsers[1].Id, entities!.First().Rehearsals!.First().PersonRehearsals!.First().AppUser!.Id);
        }
        
        [Fact]
        public async void UpdatePersonRehearsalTest()
        {
            // ARRANGE
            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            
            var getSeededResponse = await _client.GetAsync("/api/v1/Projects");
            
            getSeededResponse.EnsureSuccessStatusCode();

            var seededBody = await (getSeededResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededBody);

            var seededEntity = JsonHelper.DeserializeWithWebDefaults<List<Project>>(seededBody)!.First();
            Assert.NotNull(seededEntity);
            Assert.Equal("seededProjectName", seededEntity!.Name);
            
            var uri = "/api/v1/rehearsals";

            var nowTime = DateTime.Now;
            var thenTime = DateTime.Now.AddHours(1);
            var entity = new PublicApi.DTO.v1.RehearsalAddDTO()
            {
                Info = "info",
                RehearsalProgramme = "asdasd",
                Location = "loc",
                Start = nowTime,
                End = thenTime,
                ProjectId = seededEntity.Id
            };

            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var rehearsal = JsonHelper.DeserializeWithWebDefaults<RehearsalDTO>(body);
            
            
            Assert.NotNull(rehearsal);
            Assert.NotEqual(Guid.Empty, rehearsal!.Id);
            Assert.Equal("info", rehearsal!.Info);
            Assert.Equal("asdasd", rehearsal!.RehearsalProgramme);
            Assert.Equal("loc", rehearsal!.Location);
            Assert.Equal(nowTime, rehearsal!.Start);
            Assert.Equal(thenTime, rehearsal!.End);
            Assert.Equal(seededEntity.Id, rehearsal!.ProjectId);
            
            uri = "/api/v1/personrehearsals";
            
            var usersResponse = await _client.GetAsync("/api/v1/Account/GetUsers");
            
            usersResponse.EnsureSuccessStatusCode();

            var seededUsers = await (usersResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededUsers);


            var appUsers = JsonHelper.DeserializeWithWebDefaults<List<AppUser>>(seededUsers);
            Assert.NotNull(appUsers);
            Assert.NotEqual(Guid.Empty, appUsers![0].Id);

            var personRehearsal = new PersonRehearsalAddDTO()
            {
                AppUserId = appUsers[0].Id,
                RehearsalID = rehearsal.Id
            };
            
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personRehearsal), Encoding.Default, "application/json"));

            var asd = getTestResponse.Content.ReadAsStringAsync().Result;
            getTestResponse.EnsureSuccessStatusCode();

            
            body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<PersonRehearsalDTO>(body);
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("pending", data!.Status);
            Assert.Equal(rehearsal.Id, data!.RehearsalID);
            Assert.Equal(appUsers[0].Id, data!.AppUserId);
            
            var updatedPersonRehearsal = new PersonRehearsalAddDTO()
            {
                AppUserId = appUsers[0].Id,
                RehearsalID = rehearsal.Id,
                Status = "accepted",
                Comment = "samplecomment"
            };
            uri = uri + "/" +  data!.Id;
            
            getTestResponse = await _client.PutAsync(uri, new StringContent(JsonConvert.SerializeObject(updatedPersonRehearsal), Encoding.Default, "application/json"));

            getTestResponse.EnsureSuccessStatusCode();

            
            var getUpdatedResponse = await _client.GetAsync("/api/v1/PersonRehearsals");
            
            getUpdatedResponse.EnsureSuccessStatusCode();

            var updatedBody = await (getUpdatedResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(updatedBody);

            var updatedEntity = JsonHelper.DeserializeWithWebDefaults<List<PersonRehearsal>>(updatedBody)!.First();
            
            Assert.NotNull(updatedEntity);
            Assert.Equal(data!.Id, updatedEntity.Id);
            Assert.Equal("accepted", updatedEntity.Status);
            Assert.Equal("samplecomment", updatedEntity.Comment);

        }

        
        [Fact]
        public async void UpdatePersonConcertTest()
        {
            // ARRANGE
            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            
            var getSeededResponse = await _client.GetAsync("/api/v1/Projects");
            
            getSeededResponse.EnsureSuccessStatusCode();

            var seededBody = await (getSeededResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededBody);

            var seededEntity = JsonHelper.DeserializeWithWebDefaults<List<Project>>(seededBody)!.First();
            Assert.NotNull(seededEntity);
            Assert.Equal("seededProjectName", seededEntity!.Name);
            
            var uri = "/api/v1/concerts";

            var nowTime = DateTime.Now;
            var entity = new PublicApi.DTO.v1.ConcertAddDTO()
            {
                Name = "name",
                Info = "info",
                Programme = "asdasd",
                Location = "loc",
                Start = nowTime,
                ProjectId = seededEntity.Id
            };

            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var concert = JsonHelper.DeserializeWithWebDefaults<ConcertDTO>(body);
            
            
            Assert.NotNull(concert);
            Assert.NotEqual(Guid.Empty, concert!.Id);
            Assert.Equal("name", concert!.Name);
            Assert.Equal("info", concert!.Info);
            Assert.Equal("asdasd", concert!.Programme);
            Assert.Equal("loc", concert!.Location);
            Assert.Equal(nowTime, concert!.Start);
            Assert.Equal(seededEntity.Id, concert!.ProjectId);
            
            
            uri = "/api/v1/personconcerts";
            
            var usersResponse = await _client.GetAsync("/api/v1/Account/GetUsers");
            
            usersResponse.EnsureSuccessStatusCode();

            var seededUsers = await (usersResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededUsers);


            var appUsers = JsonHelper.DeserializeWithWebDefaults<List<AppUser>>(seededUsers);
            Assert.NotNull(appUsers);
            Assert.NotEqual(Guid.Empty, appUsers![0].Id);

            var personConcert = new PersonConcertAddDTO()
            {
                AppUserId = appUsers[0].Id,
                ConcertId = concert.Id
            };
            
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personConcert), Encoding.Default, "application/json"));

            getTestResponse.EnsureSuccessStatusCode();

            
            body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<PersonConcertDTO>(body);
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("pending", data!.Status);
            Assert.Equal(concert.Id, data!.ConcertId);
            Assert.Equal(appUsers[0].Id, data!.AppUserId);
            
            var updatedPersonConcert = new PersonConcertAddDTO()
            {
                AppUserId = appUsers[0].Id,
                ConcertId = concert.Id,
                Status = "accepted",
                Comment = "samplecomment"
            };
            uri = uri + "/" +  data!.Id;
            
            getTestResponse = await _client.PutAsync(uri, new StringContent(JsonConvert.SerializeObject(updatedPersonConcert), Encoding.Default, "application/json"));

            getTestResponse.EnsureSuccessStatusCode();

            
            var getUpdatedResponse = await _client.GetAsync("/api/v1/PersonConcerts");
            
            getUpdatedResponse.EnsureSuccessStatusCode();

            var updatedBody = await (getUpdatedResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(updatedBody);

            var updatedEntity = JsonHelper.DeserializeWithWebDefaults<List<PersonConcert>>(updatedBody)!.First();
            
            Assert.NotNull(updatedEntity);
            Assert.Equal(data!.Id, updatedEntity.Id);
            Assert.Equal("accepted", updatedEntity.Status);
            Assert.Equal("samplecomment", updatedEntity.Comment);

        }
        
        
        [Fact]
        public async void CreatePersonProjectTest()
        {
            // ARRANGE
            var token = GetAdminJwtToken().Result;
            Assert.NotNull(token);
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);
            
            var getSeededResponse = await _client.GetAsync("/api/v1/Projects");
            
            getSeededResponse.EnsureSuccessStatusCode();

            var seededBody = await (getSeededResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededBody);

            var project = JsonHelper.DeserializeWithWebDefaults<List<Project>>(seededBody)!.First();
            Assert.NotNull(project);
            Assert.Equal("seededProjectName", project!.Name);
          
            var uri = "/api/v1/personprojects";
            
            var usersResponse = await _client.GetAsync("/api/v1/Account/GetUsers");
            
            usersResponse.EnsureSuccessStatusCode();

            var seededUsers = await (usersResponse).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(seededUsers);

            var appUsers = JsonHelper.DeserializeWithWebDefaults<List<AppUser>>(seededUsers);
            Assert.NotNull(appUsers);
            Assert.NotEqual(Guid.Empty, appUsers![0].Id);

            var personProject = new PersonProjectDTO()
            {
                AppUserId = appUsers[1].Id,
                ProjectId = project.Id
            };
            
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personProject), Encoding.Default, "application/json"));

            var asd = getTestResponse.Content.ReadAsStringAsync().Result;
            getTestResponse.EnsureSuccessStatusCode();

            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<PersonProjectDTO>(body);
            Assert.NotNull(data);
            Assert.NotEqual(Guid.Empty, data!.Id);
            Assert.Equal("pending", data!.Status);
            Assert.Equal(project.Id, data!.ProjectId);
            Assert.Equal(appUsers[1].Id, data!.AppUserId);
            
            uri = "/api/v1/personprojects/bulk";

            var personProjects = new List<PersonProjectDTO>();
            personProjects.Add(new PersonProjectDTO()
            {
                AppUserId = appUsers[2].Id,
                ProjectId = project.Id
            });
            
            personProjects.Add(new PersonProjectDTO()
            {
                AppUserId = appUsers[0].Id,
                ProjectId = project.Id
            });
            
            getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(personProjects), Encoding.Default, "application/json"));
            getTestResponse.EnsureSuccessStatusCode();
            
            
            var getCreatedEntities = await _client.GetAsync("/api/v1/projects");
            
            getCreatedEntities.EnsureSuccessStatusCode();

            var responseBody = await (getCreatedEntities).Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(responseBody);

            var entities = JsonHelper.DeserializeWithWebDefaults<List<ProjectDTO>>(responseBody);
            Assert.NotNull(entities);
            Assert.Equal(3, entities!.First().PersonProjects!.Count());
            
            Assert.Equal("pending", entities!.First().PersonProjects!.First().Status);
            Assert.Equal(appUsers[1].Id, entities!.First().PersonProjects!.First().AppUser!.Id);
        }
        
        
        
        
        //Method that returns admin jwt token
        private async Task<string> GetAdminJwtToken()
        {               
            var uri = "/api/v1/Account/login";

            var loginDto = new Login()
            {
                Email = "akaver@akaver.com",
                Password = "Test.test.2020",
            };
            // ACT
            var getTestResponse = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.Default, "application/json"));
            
            getTestResponse.EnsureSuccessStatusCode();
            
            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);
            
            Assert.NotNull(data);
            Assert.NotNull(data!.Token);
            
            return data!.Token;
        }
    }
}