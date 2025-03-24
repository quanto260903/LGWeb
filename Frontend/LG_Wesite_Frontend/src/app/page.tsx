import Image from "next/image";
import Slider from "../components/slider/Slider";
import Partner from "../components/Partner";
import AboutUs from "../components/AboutUs";
import Recruitment from "../components/CareersRecruitment";
import Service from "../components/Service";
import Applied from "../components/Appliedtools";
import Product from "../components/Products";
export default function Home() {
  return (<>

    {/* slider */}
    <Slider />

    {/* about us */}
    <AboutUs />

    {/* Product */}
    
      <Product/>
    
    <Recruitment isHomePage={true} showButton={true}  isScrollable={true} itemLimit={3}/>
  </>

  );
}
