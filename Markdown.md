# Chosen setup
We decided to make a high-available setup with a hot and standby server for our clone, as the other option wasn't actually possible because of the way we made our clone.

# Explaination  
We used AWS so the first step we made, was to create an elastic IP. This is because if we restarted our instance it gets a new IP, so this is essentially a workaround to avoid the IP issue.

For the second step, we created an image of the instance.

For the third step, we created a launch configuration.

Lastly, we created an auto scaling group that creates instances in case one of them goes down.
