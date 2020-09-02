# 200902-dotnet-lecture
Data Annotations and eager loading

## Use the provided starter MVC project
* Run and apply migrations
* Verify database created
* Verify application launches

### Bands and Albums

#### Band
```
id - int, required
BandName - string, required
MusicGenre - string, required
Albums - List<Album>
```

#### Album
```
id - int, required
AlbumTitle - string, required
AlbumDescription - string, required, min length 50, max length 1000
AlbumRating - int, range 1 - 5
```

### Implement Create and Read endpoints
* Adding a new Album should require a Band to tie the album to
  * Do an band lookup on ID passed in, return error if band not found
* Get band endpoint should also return all albums for each band
* Return list of validations failures on failure

### Extra
Use this method for handling validation errors.
```
        public static List<string> GetErrorListFromModelState
                                              (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
```        
