'use client';

import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import Image from 'next/image';
import Link from 'next/link';
import '../../css/pages/_detailcareers.scss';
import bannercareer from '../../../app/images/careers_banner.png'
import net from '../../../app/images/NetcareerImage.png'
import emailicon from '../../../app/images/icon-email.png'
import phoneicon from '../../../app/images/icon-phone.png'
import icoFb from '../../../app/images/ico-fb.png';
import icoLinkedIn from '../../../app/images/ico-linkedin.png';
import salary from '../../../app/images/salary.png';
import working from '../../../app/images/working.png';
import gender from '../../../app/images/gender.png';
import number from '../../../app/images/number.png';
import rank from '../../../app/images/rank.png';
import experience from '../../../app/images/experience.png';
import CheckmarkCircle from '../../../app/images/CheckmarkCircle.png';
import '../../css/abstracts/_variables.scss';
import Recruitment from "../../../components/CareersRecruitment";

import { useSearchParams } from 'next/navigation';  // Import useSearchParams
import { BlogCategoryType } from '@/type/BlogCategory';
import { GetCareersJobOpp } from '@/api/careers';

const CareerDetail = () => {
  const searchParams = useSearchParams();  // Hook để lấy searchParams từ URL
  const id = searchParams.get('id');  // Lấy 'id' từ URL query string
  
  const [data, setData] = useState<BlogCategoryType | null>(null); // Dữ liệu trả về là 1 đối tượng, không phải mảng
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (id) { // Nếu id tồn tại
      const loadData = async () => {
        try {
          setLoading(true);
          const result = await GetCareersJobOpp(Number(id)); // Gọi API với id
          setData(result); // Cập nhật dữ liệu khi nhận được kết quả
        } catch (error) {
          console.error('Error fetching job details:', error);
          setError('Failed to fetch job details');
        } finally {
          setLoading(false);
        }
      };
      loadData();
    }
  }, [id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  if (!data) {
    return <div>Job not found</div>;
  }

  return (
    <div className="career-detail">
      {/* Banner Section */}
      <div className="about-container-img">
        <div className="banner">
          <Link href={'/'}>
            <Image
              src={bannercareer}
              alt="Careers Background"
              className="background-image"
            />
          </Link>
        </div>
        <div className="overlay-text">Careers</div>
      </div>

      {/* Job Details Section */}
      <div className="job-detail-section wrp">
        <div className="detail-container">
          {/* Display job details */}
          <div dangerouslySetInnerHTML={{ __html: data.detail }} />
          {/* You can add more fields here like salary, requirements, etc. */}
        </div>
      </div>

      {/* Other Job Opportunities Section */}
      {/* Assuming you have the Recruitment component */}
      <Recruitment isHomePage={true} showButton={false} isScrollable={true} itemLimit={6} />
    </div>
  );
};

export default CareerDetail;
