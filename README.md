## Spotify Plus

Spotify Plus is an extension to the music listening service Spotify that allows users to see statistics about the listening habits, such as their top tracks, top artists, and top genres.

## Install Instructions

**REQUIRED**: Email rlevisky@uccs.edu with the Spotify email being used to test the application! The Spotify API connection is in development mode (it's the only free option) and I need to manually add allowed users to the Spotify API. The app **WILL NOT** work unless the account is added to the interface. 
An .exe is included with the submission. It is a standalone executable- there is no need to install or download anything other than the executable. It does not need to be ran with administrator, and any firewall prompts it generates can be safely ignored. 

## Code Conventions

- Spotify Plus was coded in the C# language within Microsoft's Visual Studio IDE. 
- The code is 842 lines long.
- The code spans across 8 files.

## API Connection

Spotify Plus utilizes the Spotify API in order to fetch the necessary data for the application. The user starts by logging into their existing Spotify account, after which the API calls will be made and the neccessary information will be displayed. 

## Program Flow (Backend)

After successfully connecting to the Spotify API, the code makes calls to the API to get the user's top 10 tracks, top 5 genres, and the top 5 artists. After recieving the responses for these API calls, the information is packaged into 3 separate timeframes for each: short, medium, and long. This is done by iterating through each response and placing the necessary information into its own list. For tracks, the title of the song, the artists who contributed to the song, and the cover image are recieved. For artists, the name of the artist and the artist's image is recieved. Genres are a part of the artists call in the API, but they are packaged separately since the number of each genre needs to be counted. 

## Program Flow (Frontend)

Once the API calls have been completed in the backend, the user's top tracks, artists, and genres are displayed to the user using Winforms. The user can select which timeframe to view after connecting their account. 
