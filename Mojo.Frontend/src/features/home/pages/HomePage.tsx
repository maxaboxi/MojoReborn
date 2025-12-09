import { Box, Button, Stack, Typography, Card, CardContent, Chip } from '@mui/material';
import ExploreIcon from '@mui/icons-material/Explore';
import ArticleIcon from '@mui/icons-material/Article';
import GroupsIcon from '@mui/icons-material/Groups';
import RocketLaunchIcon from '@mui/icons-material/RocketLaunch';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '@features/auth/providers/AuthProvider';
import './HomePage.css';

const highlights = [
  {
    icon: <ArticleIcon fontSize="large" />,
    title: 'Modernized blog engine',
    description: 'Craft long-form stories with a sleek editor and flexible theme system.',
  },
  {
    icon: <GroupsIcon fontSize="large" />,
    title: 'Community ready',
    description: 'Forums, comments, and roles unify every site inside the network.',
  },
  {
    icon: <RocketLaunchIcon fontSize="large" />,
    title: 'Migration friendly',
    description: 'Legacy mojoPortal users can carry their roles and content forward securely.',
  },
];

export const HomePage = () => {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();

  const handlePrimaryCta = () => {
    navigate('/blog');
  };

  const handleSecondaryCta = () => {
    navigate(isAuthenticated ? '/blog/create' : '/auth/login?redirect=/blog/create');
  };

  return (
    <Box className="home-page">
      <Box className="home-hero">
        <Stack spacing={3} alignItems="flex-start">
          <Chip icon={<ExploreIcon />} label="Mojo Reborn" color="primary" variant="outlined" />
          <Typography variant="h2" component="h1" className="home-hero-title">
            Welcome to the next chapter of MojoPortal
          </Typography>
          <Typography variant="body1" className="home-hero-subtitle">
            A modern .NET experience for running content-heavy sites, migrating legacy users, and building community experiences.
          </Typography>
          <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2} width="100%">
            <Button variant="contained" size="large" onClick={handlePrimaryCta} className="home-cta-primary">
              Explore the blog
            </Button>
            <Button variant="outlined" size="large" onClick={handleSecondaryCta}>
              {isAuthenticated ? 'Create a post' : 'Sign in to contribute'}
            </Button>
          </Stack>
        </Stack>
      </Box>

      <Box className="home-highlight-grid">
        {highlights.map((item) => (
          <Card key={item.title} className="home-highlight-card" elevation={3}>
            <CardContent>
              <Box className="home-highlight-icon">{item.icon}</Box>
              <Typography variant="h6" component="h3" gutterBottom>
                {item.title}
              </Typography>
              <Typography variant="body2" color="text.secondary">
                {item.description}
              </Typography>
            </CardContent>
          </Card>
        ))}
      </Box>
    </Box>
  );
};
