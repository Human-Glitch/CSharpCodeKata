pipeline 
{
    agent any
    stages 
	{
        stage('build') 
		{
            steps 
			{
                withMaven(maven: 'maven_3_5_0')
				{
					echo 'build'
				}
            }
        }

		stage('deploy') 
		{
			steps 
			{
				withMaven(maven: 'maven_3_5_0')
				{
					echo 'deploy'
				}
			}
			
		}
	}
}