import { BrowserRouter } from 'react-router-dom';
import MainRouter from './MainRouter';
import AppHeader from './core/AppHeader';

function App () {
  return (
    <div>
      <BrowserRouter>
        <AppHeader />
        <div className='container'>
          <MainRouter />
        </div>
      </BrowserRouter>
    </div>
  );
}

export default App;
