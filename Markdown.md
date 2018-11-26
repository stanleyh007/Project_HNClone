# Chosen setup
We decided to make a high-available setup with a hot and standby server for our clone, as the other option wasn't actually possible because of the way we made our clone.

# Explaination  
We are using AWS so the first step is to create an elastic IP because if your restart an instance it gets a new IP so to get around that we need to use an elastic IP.  
Step two we create an image of the instance.  
Step three we create a launch configuration.  
Step four we create an auto scaling group, it creates instances in case one goes down.
