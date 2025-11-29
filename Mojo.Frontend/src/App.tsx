import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider } from './theme/ThemeProvider';
import { AppLayout } from './components/AppLayout/AppLayout';
import { BlogList } from './features/blog/pages/BlogList';
import { BlogPostDetail } from './features/blog/pages/BlogPostDetail';
import { CreateBlogPost } from './features/blog/pages/CreateBlogPost';
import { EditBlogPost } from './features/blog/pages/EditBlogPost';
import './App.css';

function App() {
  return (
    <ThemeProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<AppLayout />}>
            <Route index element={<Navigate to="/blog" replace />} />
            <Route path="blog" element={<BlogList />} />
            <Route path="blog/post/:id" element={<BlogPostDetail />} />
            <Route path="blog/create" element={<CreateBlogPost />} />
            <Route path="blog/edit/:id" element={<EditBlogPost />} />
            <Route path="forum" element={<div>Forum - Coming Soon</div>} />
            <Route path="admin" element={<div>Admin - Coming Soon</div>} />
            <Route path="*" element={<Navigate to="/blog" replace />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;
