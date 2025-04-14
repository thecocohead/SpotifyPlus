using EmbedIO.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyAPI.Web;
using SpotifyPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlus.Tests
{
    [TestClass()]
    public class SpotifyAPITests
    {
        // Package Top Artists Tests 
        [TestMethod()]
        public void PackageTopArtistsEmptyTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopArtistsResponse mockResponse = new UsersTopArtistsResponse();
            mockResponse.Items = new List<FullArtist>();
            //Act
            var result = api.PackageTopArtists(mockResponse);

            //Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod()]
        public void PackageTopArtistsFullTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopArtistsResponse mockResponse = new UsersTopArtistsResponse();
            mockResponse.Items = new List<FullArtist>
            {
                new FullArtist
                {
                    Name = "Test Artist",
                    Genres = new List<string> { "Pop", "Rock" },
                    Followers = new Followers { Total = 1000 },
                    Images = new List<Image> { new Image { Url = "http://example.com/image.jpg" } }
                },
                new FullArtist
                {
                    Name = "Another Artist",
                    Genres = new List<string> { "Jazz" },
                    Followers = new Followers { Total = 500 },
                    Images = new List<Image> { new Image { Url = "http://example.com/another_image.jpg" } }
                },
                new FullArtist
                {
                    Name = "Third Artist",
                    Genres = new List<string> { "Classical" },
                    Followers = new Followers { Total = 2000 },
                    Images = new List<Image> { new Image { Url = "http://example.com/third_image.jpg" } }
                },
                new FullArtist
                {
                    Name = "Fourth Artist",
                    Genres = new List<string> { "Hip-Hop" },
                    Followers = new Followers { Total = 3000 },
                    Images = new List<Image> { new Image { Url = "http://example.com/fourth_image.jpg" } }
                },
                new FullArtist
                {
                    Name = "Fifth Artist",
                    Genres = new List<string> { "Country" },
                    Followers = new Followers { Total = 4000 },
                    Images = new List<Image> { new Image { Url = "http://example.com/fifth_image.jpg" } }
                }
            };

            //Act
            List<ArtistInfo> result = api.PackageTopArtists(mockResponse);

            //Assert
            Assert.AreEqual(mockResponse.Items.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Name, mockResponse.Items[i].Name);
                Assert.AreEqual(result[i].Image, mockResponse.Items[i].Images[0].Url);
            }
        }

        [TestMethod()]
        public void PackageTopTestsMultiArtist()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopTracksResponse mockResponse = new UsersTopTracksResponse();
            mockResponse.Items = new List<FullTrack>
            {
                new FullTrack {
                    Name = "Test Track 1",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 1-1"
                        },
                        new SimpleArtist
                        {
                            Name = "Test Artist 1-2"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 2",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 2-1"
                        },
                        new SimpleArtist
                        {
                            Name = "Test Artist 2-2"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 3",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 3-1"
                        },
                        new SimpleArtist
                        {
                            Name = "Test Artist 3-2"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
            };
            //Act
            var result = api.PackageTopTracks(mockResponse);

            //Assert
            Assert.IsTrue(result.Count == mockResponse.Items.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Title, mockResponse.Items[i].Name);
                Assert.AreEqual(result[i].CoverImage, mockResponse.Items[i].Album.Images[0].Url);
                for (int j = 0; j < result[j].Artists.Count; j++)
                {
                    Assert.AreEqual(result[i].Artists[j], mockResponse.Items[i].Artists[j].Name);
                }
            }
        }

        //Package Top Tracks Tests

        [TestMethod()]
        public void PackageTopTracksEmptyTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopTracksResponse mockResponse = new UsersTopTracksResponse();
            mockResponse.Items = new List<FullTrack>();
            //Act
            var result = api.PackageTopTracks(mockResponse);
            //Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod()]
        public void PackageTopTracksPartialTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopTracksResponse mockResponse = new UsersTopTracksResponse();
            mockResponse.Items = new List<FullTrack>
            {
                new FullTrack {
                    Name = "Test Track 1",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 1"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 2",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 2"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 3",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 3"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                }
            };
            //Act
            var result = api.PackageTopTracks(mockResponse);

            //Assert
            Assert.IsTrue(result.Count == mockResponse.Items.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Title, mockResponse.Items[i].Name);
                Assert.AreEqual(result[i].CoverImage, mockResponse.Items[i].Album.Images[0].Url);
                for (int j = 0; j < result[j].Artists.Count; j++)
                {
                    Assert.AreEqual(result[i].Artists[j], mockResponse.Items[i].Artists[j].Name);
                }
            }
        }

        [TestMethod()]
        public void PackageTopTracksFullTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopTracksResponse mockResponse = new UsersTopTracksResponse();
            mockResponse.Items = new List<FullTrack>
            {
                new FullTrack {
                    Name = "Test Track 1",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 1"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 2",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 2"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 3",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 3"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 4",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 4"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 5",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 5"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 6",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 6"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 7",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 7"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 8",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 8"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 9",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 9"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },
                new FullTrack {
                    Name = "Test Track 0",
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist
                        {
                            Name = "Test Artist 0"
                        }
                    },
                    Album = new SimpleAlbum
                    {
                        Images = new List<Image>
                        {
                            new Image
                            {
                                Height = 640,
                                Width = 640,
                                Url = "https://i.scdn.co/image/ab67616d0000b2737bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 300,
                                Width = 300,
                                Url = "https://i.scdn.co/image/ab67616d00001e027bfbad19306705ddcd07e756"
                            },
                            new Image
                            {
                                Height = 64,
                                Width = 64,
                                Url = "https://i.scdn.co/image/ab67616d000048517bfbad19306705ddcd07e756"
                            }
                        }
                    }
                },

            };
            //Act
            var result = api.PackageTopTracks(mockResponse);

            //Assert
            Assert.IsTrue(result.Count == mockResponse.Items.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Title, mockResponse.Items[i].Name);
                Assert.AreEqual(result[i].CoverImage, mockResponse.Items[i].Album.Images[0].Url);
                for (int j = 0; j < result[j].Artists.Count; j++)
                {
                    Assert.AreEqual(result[i].Artists[j], mockResponse.Items[i].Artists[j].Name);
                }
            }
        }

        //Package Top Genres Tests

        [TestMethod()]
        public void PackageTopGenresEmptyTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopArtistsResponse mockResponse = new UsersTopArtistsResponse();
            mockResponse.Items = new List<FullArtist>();
            //Act
            List<string> response = api.PackageTopGenres(mockResponse);
            //Assert
            Assert.IsTrue(response.Count == mockResponse.Items.Count);
        }

        [TestMethod()]
        public void PackageTopGenresNonConflictingTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopArtistsResponse mockResponse = new UsersTopArtistsResponse();
            mockResponse.Items = new List<FullArtist> {
                new FullArtist
                {
                    Name = "Test Artist 1",
                    Genres = new List<string>
                    {
                        "Pop",
                        "Rock"
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 2",
                    Genres = new List<string>
                    {
                        "Jazz",
                        "Blues"
                    }
                }
            };
            //Act
            List<string> response = api.PackageTopGenres(mockResponse);

            //Assert
            Assert.IsTrue(response[0] == mockResponse.Items[0].Genres[0]);
            Assert.IsTrue(response[1] == mockResponse.Items[0].Genres[1]);
            Assert.IsTrue(response[2] == mockResponse.Items[1].Genres[0]);
            Assert.IsTrue(response[3] == mockResponse.Items[1].Genres[1]);
        }

        [TestMethod()]
        public void PackageTopGenresConflictingTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopArtistsResponse mockResponse = new UsersTopArtistsResponse();
            mockResponse.Items = new List<FullArtist> {
                new FullArtist
                {
                    Name = "Test Artist 1",
                    Genres = new List<string>
                    {
                        "Pop",
                        "Rock"
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 2",
                    Genres = new List<string>
                    {
                        "Pop",
                        "Jazz"
                    }
                }
            };
            //Act
            List<string> response = api.PackageTopGenres(mockResponse);
            //Assert
            Assert.IsTrue(response[0] == "Pop");
            Assert.IsTrue(response[1] == "Rock");
            Assert.IsTrue(response[2] == "Jazz");
        }

        [TestMethod()]
        public void PackageTopGenresSizeTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopArtistsResponse mockResponse = new UsersTopArtistsResponse();
            mockResponse.Items = new List<FullArtist> {
                new FullArtist
                {
                    Name = "Test Artist 1",
                    Genres = new List<string>
                    {
                        "Pop",
                        "Rock"
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 2",
                    Genres = new List<string>
                    {
                        "Blues",
                        "Jazz"
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 2",
                    Genres = new List<string>
                    {
                        "Punk",
                        "Metal"
                    }
                }
            };
            //Act
            List<string> response = api.PackageTopGenres(mockResponse);

            //Assert
            Assert.IsTrue(response.Count == 5);
            Assert.IsTrue(response[0] == mockResponse.Items[0].Genres[0]);
            Assert.IsTrue(response[1] == mockResponse.Items[0].Genres[1]);
            Assert.IsTrue(response[2] == mockResponse.Items[1].Genres[0]);
            Assert.IsTrue(response[3] == mockResponse.Items[1].Genres[1]);
            Assert.IsTrue(response[4] == mockResponse.Items[2].Genres[0]);
        }

        [TestMethod()]
        public void PackageTopGenresTopTest()
        {
            //Arrange
            SpotifyAPI api = new SpotifyAPI();
            UsersTopArtistsResponse mockResponse = new UsersTopArtistsResponse();
            mockResponse.Items = new List<FullArtist> {
                new FullArtist
                {
                    Name = "Test Artist 1",
                    Genres = new List<string>
                    {
                        "Punk",
                        "Jazz",
                        "Rock", 
                        "Blues",
                        "Pop"
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 2",
                    Genres = new List<string>
                    {
                        "Rock",
                        "Jazz",
                        "Blues",
                        "Pop",
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 3",
                    Genres = new List<string>
                    {
                        "Pop",
                        "Blues",
                        "Jazz",
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 4",
                    Genres = new List<string>
                    {
                        "Jazz",
                        "Pop",
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 5",
                    Genres = new List<string>
                    {
                        "Pop",
                    }
                },
                new FullArtist
                {
                    Name = "Test Artist 6",
                    Genres = new List<string>
                    {
                        "Metal"
                    }
                },
            };
            //Act
            List<string> response = api.PackageTopGenres(mockResponse);
            //Assert
            Assert.IsTrue(response.Count == 5);
            Assert.IsTrue(response[0] == "Pop");
            Assert.IsTrue(response[1] == "Jazz");
            Assert.IsTrue(response[2] == "Blues");
            Assert.IsTrue(response[3] == "Rock");
            Assert.IsTrue(response[4] == "Punk");
        }

    }
}